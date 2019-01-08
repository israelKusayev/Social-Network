import React from 'react';
import { NavLink } from 'react-router-dom';
import SearchUsers from '../searchUsers';

function MainNav() {
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
            <i className="fa fa-bell" />
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

export default MainNav;
