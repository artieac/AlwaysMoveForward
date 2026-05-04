import jQuery from 'jquery';

export class RestClient {
     getRequest(url, responseHandler) {
        jQuery.ajax({
             headers: {
                     'Accept': 'application/json',
                     'Content-Type': 'application/json'
             },
             type: "GET",
             url: url,
             async: true,
             dataType: 'json',
             success: function(data) {
                   responseHandler(true, data);
              },
             error: function(xhr, status, err) {
                   responseHandler(false);
             }
       });
    }

    postRequest(url, params, responseHandler){
        if(isValid(params)){
            jQuery.ajax({
                  headers: {
                          'Accept': 'application/json',
                          'Content-Type': 'application/json'
                  },
                  type: "POST",
                  url: url,
                  data: JSON.stringify(params),
                  success: function(data) {
                        responseHandler(true, data);
                   },
                  error: function(xhr, status, err) {
                        responseHandler(false);
                  }
            });
        }
        else {
            jQuery.ajax({
                  headers: {
                          'Accept': 'application/json',
                          'Content-Type': 'application/json'
                  },
                  type: "POST",
                  url: url,
                  success: function(data) {
                        responseHandler(true, data);
                   },
                  error: function(xhr, status, err) {
                        responseHandler(false);
                  }
            });
        }
    }

    putRequest(url, params, responseHandler){
        if(isValid(params)){
            jQuery.ajax({
                  headers: {
                          'Accept': 'application/json',
                          'Content-Type': 'application/json'
                  },
                  type: "PUT",
                  url: url,
                  data: JSON.stringify(params),
                  success: function(data) {
                        responseHandler(true, data);
                   },
                  error: function(xhr, status, err) {
                        responseHandler(false);
                  }
            });
        }
        else {
            jQuery.ajax({
                  headers: {
                          'Accept': 'application/json',
                          'Content-Type': 'application/json'
                  },
                  type: "PUT",
                  url: url,
                  success: function(data) {
                        responseHandler(true, data);
                   },
                  error: function(xhr, status, err) {
                        responseHandler(false);
                  }
            });
        }
    }

    deleteRequest(url, responseHandler){
        jQuery.ajax({
              headers: {
                      'Accept': 'application/json',
                      'Content-Type': 'application/json'
              },
              type: "DELETE",
              url: url,
              success: function(data) {
                    responseHandler(true, data);
               },
              error: function(xhr, status, err) {
                    responseHandler(false);
              }
        });
    }
}