import React from 'react';
import { NavLink } from 'react-router-dom';
import { getJwt } from '../../services/jwtService';
import LoginRegisterNav from './loginRegisterNav';
import MainNav from './mainNav';

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
        {getJwt() ? <MainNav /> : <LoginRegisterNav />}
      </div>
    </nav>
  );
}

export default Navbar;
