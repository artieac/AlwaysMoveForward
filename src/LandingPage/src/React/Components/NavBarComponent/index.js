import React from 'react';
import PropTypes from 'prop-types'
import { connect, useSelector } from "react-redux";
import { Link } from 'react-router-dom';
import "./component.css"

const NavBarComponent = ({ title, navBarElements, currentPage, currentUser }) => {
    const workflowState = useSelector((state) => state.workflowState);
    const videoReducer = useSelector((state) => state.videoReducer);

    const onSignInClick = () => {
        let loginUrl = "/login";
        let nextConnector = "?";

        if(workflowState.currentPage!='undefined' && workflowState.currentPage!=null && workflowState.currentPage!=""){
            loginUrl += nextConnector + "sourcePage=" + workflowState.currentPage;
            nextConnector = "&";
        }

        if(videoReducer.currentVideo!=null && videoReducer.currentVideo.id!=null){
            loginUrl += nextConnector + "videoId=" + videoReducer.currentVideo.id;
        }

        window.open(loginUrl, "_self");
    }

    const renderLoginElement = () => {
        if(currentUser!=null && currentUser!='undefined' && currentUser.id != null && currentUser.id > 0){
            return (
                    <Link className="nav-link" aria-current="page" to="/user" > { currentUser.name }</Link>
            );
        } else {
            return (
                <a className="nav-link" aria-current="page" onClick={ onSignInClick } >Log In</a>
            );
        }
    }

    return (
        <nav className="navbar navbar-default">
            <div className="container-fluid">
                <a className="navbar-brand" href="#">{ title }</a>
                <div className="navbar-header">
                    <button type="button" className="navbar-toggler" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="icon-bar"></span>
                        <span className="icon-bar"></span>
                        <span className="icon-bar"></span>
                    </button>
                </div>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0" id="navbarSupportedContent">
                        {navBarElements.map((item, index) => (
                            <li key={ index } className={ item.label==currentPage ? "nav-item active" : "nav-item"}>
                                <Link className="nav-link" aria-current="page" to={ item.target } >{ item.label }</Link>
                            </li>
                        ))}
                        <li className="nav-item">
                            { renderLoginElement() }
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    );
}

NavBarComponent.propTypes = {
    title: PropTypes.string,
    navBarElements: PropTypes.array,
    currentPage: PropTypes.string,
    currentUser: PropTypes.object
}

export default connect(null, null)(NavBarComponent);