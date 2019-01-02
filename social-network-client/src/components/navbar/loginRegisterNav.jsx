import React from 'react';
import { NavLink } from 'react-router-dom';
import FacebookLogin from '../facebookLoginBtn';
import { facebookLogin as onLogin } from '../../services/authService';

function LoginRegisterNav() {
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
        <FacebookLogin onLogin={onLogin} className="nav-link mt-3" />
      </li>
    </ul>
  );
}

export default LoginRegisterNav;
