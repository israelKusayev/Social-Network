import React from 'react';
import { NavLink } from 'react-router-dom';
import FacebookLogin from './facebookLoginBtn';

function Navbar() {
  return (
    <nav className="navbar navbar-expand-sm bg-dark navbar-dark">
      <NavLink className="navbar-brand" to="/">
        Social network
      </NavLink>
      <button
        className="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#navbarNav"
        aria-controls="navbarNav"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span className="navbar-toggler-icon" />
      </button>
      <div className="collapse navbar-collapse" id="navbarNav">
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
            <FacebookLogin className="nav-link mt-3" />
          </li>
        </ul>
      </div>
    </nav>
  );
}

export default Navbar;
