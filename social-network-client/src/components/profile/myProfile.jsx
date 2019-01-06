import React, { Component } from 'react';
import User from '../../models/user';
import UserProfile from '../userProfile';
import { deleteJwt, getJwt, getUsername, getUserId } from '../../services/jwtService';
import { Put, Get } from '../../services/httpService';
import { refreshToken } from '../../services/authService';
import { convertJsonToUser } from '../../converters/userConvertor';

export default class MyProfile extends Component {
  state = {
    user: new User()
  };
  identityUrl = process.env.REACT_APP_IDENTITY_URL;

  componentDidMount = () => {
    this.getUser();
  };

  getUser = async () => {
    const res = await Get(`${this.identityUrl}UsersIdentity/${getUserId()}`, getJwt());
    if (res.status === 401) {
      await refreshToken();
      this.getUser();
    }
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

  render() {
    const { user } = this.state;

    return <UserProfile onChange={this.handleChange} updateProfile={this.handleSubmit} user={user} />;
  }
}
