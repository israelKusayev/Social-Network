import React from 'react';
import { NavLink } from 'react-router-dom';

function MainNav() {
  return (
    <ul className="navbar-nav ml-auto">
      <li className="nav-item ">
        <NavLink className="nav-link" to="/feed">
          Feed
        </NavLink>
      </li>
      <li className="nav-item ">
        <NavLink className="nav-link" to="/create-post">
          Create post
        </NavLink>
      </li>
      <li className="nav-item ">
        <NavLink className="nav-link" to="/notifications">
          Notifications
        </NavLink>
      </li>
      <li className="nav-item">
        <NavLink className="nav-link " to="/profile">
          Profile
        </NavLink>
      </li>
    </ul>
  );
}

export default MainNav;
