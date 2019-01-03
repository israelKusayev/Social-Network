import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import { withRouter } from 'react-router-dom';
import FacebookLogin from '../facebookLoginBtn';
import { facebookLogin } from '../../services/authService';

class LoginRegisterNav extends Component {
  onLogin = (res) => {
    console.log('b');

    facebookLogin(res)
      .then(() => {
        console.log('a');

        this.props.history.push('/');
      })
      .catch(() => {});
  };
  render() {
    return (
      <ul className="navbar-nav ml-auto">
        <li className="nav-item ">
          <NavLink className="nav-link" to="/register">
            Register
          </NavLink>
        </li>
        <li className="nav-item">
          <NavLink className="nav-link" to="/login">
            Login
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
