'use strict';

var Reflux = require('reflux');
var Immutable = require('immutable');
var jQuery = require('jquery');
var _ = require('lodash');
var validator = require('validator');

// Actions
var ChartActions = require("../actions/contactActions");

// Stores
//var domainStore = require("../stores/domainStore");
//var countryInfoStore = require("../stores/countryInfoStore");

// Initial state
var state = Immutable.fromJS({
    contactCollection: {},
    error: null,
    validationStatus: true
});

var clearState = state;

var prevState = state;

var chartStore = Reflux.createStore({

    createdCharts: {},
    assignedCharts: {},
    userId: "",

    init: function (user) {
        this.createdCharts = {};
        this.assignedCharts = {};
        this.userId = user;
    },

    requestTrigger: function (alwaysTrigger) {
        if (alwaysTrigger || !Immutable.is(prevState, state)) {
            prevState = state;
            this.trigger(state.toJS());
        }
    },

    onGetCreatedCharts: function (charts) {
        var parent = this;
        return jQuery.ajax({
            type: "GET",
            url: "/api/Chart?creatorId=" + this.userId,
            dataType: 'json',
            context: this
        }).then(function (result) {
            console.log(result);
            // Add the validation states as valid because it if it made it to the server it must be valid
            var contactCollection = this._initializeCollection(result, true);
            state = state.mergeDeep({
                'contactCollection': contactCollection
            });
            this._validateCollection();
            this.requestTrigger();
        }, function (error) {
            console.log(error)
        });
    },

    onGetAssignedCharts: function (charts) {
        this.assignedCharts = charts;
    },

    
    _initializeCollection: function (contactCollection, initialValidState) {
        if (contactCollection.OwnerContactInformation.InitialContactInfoView) {
            initialValidState = false;
            contactCollection.InitialContactInfoView = true;
            console.log('First time registration');
        }

        return contactCollection;
    },

    onGetContactCollection: function () {
        var parent = this;
        return jQuery.ajax({
            type: "GET",
            url: "/api/ContactCollection?altId=" + this.activeDomainAltId + "&isInitialContactCollectionView=" + false,
            dataType: 'json',
            context: this
        }).then(function (result) {
            console.log(result);
            // Add the validation states as valid because it if it made it to the server it must be valid
            var contactCollection = this._initializeCollection(result, true);
            state = state.mergeDeep({
                'contactCollection': contactCollection
            });
            this._validateCollection();
            this.requestTrigger();
        }, function (error) {
            console.log(error)
        });
    },

    onClearContactCollection: function () {
        state = clearState;
        prevState = clearState;
    },

    onSaveContactCollection: function () {
        var activeDomainAltId = Object.keys(this.activeDomain)[0];
        var domainNeedsRegistering = state.getIn(['contactCollection', 'InitialContactInfoView']);
        var httpMethod = domainNeedsRegistering ? "POST" : "PUT";
        return jQuery.ajax({
            type: httpMethod,
            url: "/api/ContactCollection?altId=" + this.activeDomainAltId,
            contentType: "application/json",
            data: JSON.stringify(state.get('contactCollection')),
            context: this
        }).then(function (result) {
            ContactCollectionActions.saveContactCollection.completed(result);
        }, function (req, status, error) {
            ContactCollectionActions.saveContactCollection.failed(error);
        });
    },

    _validateCollection: function (updatedContactNames) {
        var validateContact = function (contactSetName) {
            var collectionIsValid = true;
            _.forEach(this.validatedFields, function (fieldName) {
                var fieldValue = state.getIn(['contactCollection', contactSetName, fieldName]);
                var country = state.getIn(['contactCollection', contactSetName, 'Country']);
                var fieldIsValid = true;
                if (fieldName !== "StreetAddress2" && // The only not required field
                    (!fieldValue || fieldValue.trim().length == 0 || fieldValue.trim().length > this.maxLength[fieldName])) {
                    fieldIsValid = false;
                } else {
                    switch (fieldName) {
                        case "PostalCode":
                            // For NL, make sure we don't have any spaces
                            if ((country === "NL" &&
                                validator.matches(fieldValue, /.*\s+.*/)) ||
                                (country === "CA" &&
                                !validator.matches(fieldValue, /^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[A-Za-z]{1} *\d{1}[A-Za-z]{1}\d{1}$/)) ||
                                (country === "US" &&
                                !validator.matches(fieldValue, /^[0-9]{5}(?:-[0-9]{4})?$/))) {
                                fieldIsValid = false;
                            }
                            break;
                        case "EmailAddress":
                            if (!validator.isEmail(fieldValue)) {
                                fieldIsValid = false;
                            }
                            break;
                        case "PhoneNumber":
                            if (!validator.matches(fieldValue, /^\+\d{1,3}\.\d{7,10}(x\d{0,4})?$/)) {
                                fieldIsValid = false;
                            }
                            break;
                    }
                }
                state = state.setIn(['contactCollection', contactSetName, 'validFields', fieldName], fieldIsValid);
                if (!fieldIsValid && collectionIsValid) {
                    collectionIsValid = false;
                }
            }.bind(this));
            state = state.setIn(['contactCollection', contactSetName, 'isValid'], collectionIsValid);
        }.bind(this);

        if (updatedContactNames) {
            _.forEach(updatedContactNames, function (contactSetName) {
                validateContact(contactSetName);
            });
        } else {
            _.forEach(this.contactCollectionSetNames, function (contactSetName) {
                validateContact(contactSetName);
            });
        }
        var validationStatus = true;
        _.forEach(this.contactCollectionSetNames, function (contactSetName) {
            validationStatus = validationStatus && state.getIn(['contactCollection', contactSetName, 'isValid']);
        });
        state = state.set('validationStatus', validationStatus);
    },

    _getPhonePrefix: function (newCountryIsoCode) {
        var countryObj = _.find(this.countryData.countries, {
            alpha2: newCountryIsoCode
        });
        var phoneNumberPrefix = _.first(countryObj.countryCallingCodes).replace(" ", ".");
        if (phoneNumberPrefix.indexOf('.') < 0) { // If it doesn't contain a period
            phoneNumberPrefix += '.';
        }
        return phoneNumberPrefix;
    },

    _updateDefaultPhoneNumber: function (collectionName) {
        var existingNumber = state.getIn(['contactCollection', collectionName, 'PhoneNumber']);
        var newCountryIsoCode = state.getIn(['contactCollection', collectionName, 'Country']);

        if (!existingNumber || validator.matches(existingNumber, /^\+\d{1,3}\.?\d{0,3}$/)) {
            var phoneNumberPrefix = this._getPhonePrefix(newCountryIsoCode);
            state = state.setIn(['contactCollection', collectionName, 'PhoneNumber'], phoneNumberPrefix);
        }

    },

    _cloneContact: function (sourceContact, destinationContact) {
        if (sourceContact != destinationContact) {
            var source = state.getIn(['contactCollection', sourceContact]);
            state = state.setIn(['contactCollection', destinationContact], source);
        }
    },

    onUpdateContact: function (partialContactCollection) {
        var updatedCollectionNames = [];
        _.forOwn(partialContactCollection, function (collection, collectionKey) {
            updatedCollectionNames.push(collectionKey);
        });

        state = state.mergeDeep({
            'contactCollection': partialContactCollection
        });

        // Check for country updates
        _.forEach(updatedCollectionNames, function (contactName) {
            if (state.getIn(['contactCollection', contactName, 'Country']) !== prevState.getIn(['contactCollection', contactName, 'Country'])) {
                this._updateDefaultPhoneNumber(contactName);
            }
            if (state.getIn(['contactCollection', contactName, 'SameAsOwner']) &&
                state.getIn(['contactCollection', 'OwnerContactInformation']) != state.getIn(['contactCollection', contactName])) {
                state = state.setIn(['contactCollection', contactName, 'SameAsOwner'], false);
            }
        }.bind(this));

        if (updatedCollectionNames.indexOf('OwnerContactInformation') > -1) {
            _.forEach(this.contactCollectionSetNames, function (contactName) {
                if (state.getIn(['contactCollection', contactName, 'SameAsOwner'])) {
                    this._cloneContact('OwnerContactInformation', contactName);
                    // Make sure that the "cloned" one is also validated
                    updatedCollectionNames.push(contactName);
                }
            }.bind(this));
        }

        this._validateCollection(updatedCollectionNames);
        this.requestTrigger();
    }
});

module.exports = contactCollectionStore;
