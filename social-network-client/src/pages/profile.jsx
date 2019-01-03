import React, { Component } from 'react';
import User from '../models/user';

import { Get, Put } from '../services/httpService';
import { getJwt, getUserId, getUsername } from '../services/jwtService';
import { convertJsonToUser } from '../converters/userConvertor';
import UserProfile from '../components/userProfile';
import RouteProtector from '../HOC/routeProtector';
import { refreshToken } from '../services/authService';
import ListGroup from '../components/listGroup';
import Followers from '../components/profile/followers';
import Following from '../components/profile/following';
import BlockedUsers from '../components/profile/blockedUsers';

class Profile extends Component {
  state = {
    user: new User(),
    selected: 'Profile'
  };

  identityUrl = process.env.REACT_APP_IDENTITY_URL;

  componentDidMount = () => {
    this.getUser();
  };

  getUser = async () => {
    // refreshToken();
    const res = await Get(`${this.identityUrl}UsersIdentity/${getUserId()}`, getJwt());
    var data = await res.json();
    let user = convertJsonToUser(data);
    user.username = getUsername();
    this.setState({ user });
  };

  handleChange = ({ currentTarget: input }) => {
    const user = { ...this.state.user };
    user[input.id] = input.value;
    this.setState({ user });
  };

  handleSubmit = async (e) => {
    e.preventDefault();

    refreshToken();
    const user = JSON.stringify(this.state.user);
    const res = await Put(`${this.identityUrl}UsersIdentity`, user, getJwt());

    if (res.status !== 200) {
      this.getUser();
    }
  };

  handleSelect = ({ name }) => {
    this.setState({ selected: name });
  };

  render() {
    const { user } = this.state;

    const items = [
      {
        id: '1',
        name: 'Profile',
        payload: <UserProfile onChange={this.handleChange} updateProfile={this.handleSubmit} user={user} />
      },
      { id: '2', name: 'Followers', payload: <Followers /> },
      { id: '3', name: 'Following', payload: <Following /> },
      { id: '4', name: 'BlockedUsers', payload: <BlockedUsers /> },
      { id: '5', name: 'Logout' }
    ];
    return (
      <div className="container mt-1">
        <div className="row">
          <div className="col-md-3 ">
            <ListGroup items={items} selectedItem={this.state.selected} onItemSelect={this.handleSelect} />
          </div>
          <div className="col-md-9">{items.find((i) => i.name === this.state.selected).payload}</div>
        </div>
      </div>
    );
  }
}

export default RouteProtector(Profile);
