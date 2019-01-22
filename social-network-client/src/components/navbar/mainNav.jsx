import React, { Component } from 'react';
import { withRouter } from 'react-router';
import { NavLink } from 'react-router-dom';
import SearchUsers from '../searchUsers';
import { registerUnreadCount } from '../../services/notificationsService';

class MainNav extends Component {
  state = {
    notificationsCount: 0
  };

  componentDidMount = () => {
    registerUnreadCount(this.onGetNotification);
  };

  onGetNotification = () => {
    let { notificationsCount } = this.state;
    notificationsCount++;
    this.setState({ notificationsCount });
  };

  render() {
    if (this.props.location.pathname === '/notifications' && this.state.notificationsCount !== 0) {
      this.setState({ notificationsCount: 0 });
    }

    const { notificationsCount } = this.state;
    return (
      <>
        <form className="form-inline mx-3 my-lg-0">
          <SearchUsers />
        </form>
        <ul className="navbar-nav ml-auto ">
          <li className="nav-item mr-2">
            <NavLink className="nav-link" to="/feed">
              <i className="fa fa-home" />
              <span> Feed</span>
            </NavLink>
          </li>
          <li className="nav-item mr-2">
            <NavLink className="nav-link" to="/create-post">
              <i className="fa fa-plus-circle" />
              <span> Create post</span>
            </NavLink>
          </li>
          <li className="nav-item mr-2">
            <NavLink className="nav-link" to="/notifications">
              {notificationsCount ? (
                <span className="text-pink">
                  <i className="fa fa-bell" />
                  {notificationsCount}
                </span>
              ) : (
                <i className="fa fa-bell" />
              )}
              <span> Notifications</span>
            </NavLink>
          </li>
          <li className="nav-item ">
            <NavLink className="nav-link " to="/profile">
              <i className="fa fa-user-circle" />
              <span> Profile</span>
            </NavLink>
          </li>
        </ul>
      </>
    );
  }
}

export default withRouter(MainNav);
