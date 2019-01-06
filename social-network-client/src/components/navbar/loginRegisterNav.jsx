import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import { withRouter } from 'react-router-dom';
import FacebookLogin from '../facebookLoginBtn';
import { facebookLogin } from '../../services/authService';

class LoginRegisterNav extends Component {
  onLogin = (res) => {
    facebookLogin(res)
      .then(() => {
        this.props.history.push('/');
      })
      .catch(() => {});
  };
  render() {
    return (
      <ul className="navbar-nav ml-auto">
        <li className="nav-item mr-2">
          <NavLink className="nav-link" to="/register">
            <i className="fa fa-user-plus" />

            <span> Register</span>
          </NavLink>
        </li>
        <li className="nav-item">
          <NavLink className="nav-link mr-3" to="/login">
            <i className="fa fa-sign-in" />
            <span> Login</span>
          </NavLink>
        </li>
        <li className="nav-item">
          <FacebookLogin onLogin={this.onLogin} className="nav-link mt-3" />
        </li>
      </ul>
    );
  }
}

export default withRouter(LoginRegisterNav);
