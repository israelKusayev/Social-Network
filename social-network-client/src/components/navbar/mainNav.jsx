import React from 'react';
import { NavLink } from 'react-router-dom';
import { deleteJwt } from '../../services/jwtService';

function MainNav() {
  return (
    <ul className="navbar-nav ml-auto">
      <li className="nav-item ">
        <NavLink className="nav-link" to="/feed">
          Feed
        </NavLink>
      </li>
      <li className="nav-item ">
        <NavLink className="nav-link" to="/notifications">
          Notifications
        </NavLink>
      </li>
      <li className="nav-item">
        <NavLink className="nav-link" to="/profile">
          Profile
        </NavLink>
      </li>
      <li className="nav-item">
        <NavLink className="nav-link" onClick={deleteJwt} to="/login">
          Logout
        </NavLink>
      </li>
    </ul>
  );
}

export default MainNav;
