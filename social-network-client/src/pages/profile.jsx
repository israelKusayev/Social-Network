import React, { Component } from 'react';
import User from '../models/user';
import { Get, Put } from '../services/httpService';
import { getJwt, getUserId, getUsername } from '../services/jwtService';
import { convertJsonToUser } from '../converters/userConvertor';
import UserProfile from '../components/userProfile';
import RouteProtector from '../HOC/routeProtector';

class Profile extends Component {
  state = {
    user: new User()
  };

  identityUrl = process.env.REACT_APP_IDENTITY_URL;

  componentDidMount = () => {
    this.getUser();
  };

  getUser = async () => {
    const res = await Get(`${this.identityUrl}UsersIdentity/${getUserId()}`, getJwt());
    var data = await res.json();
    let user = convertJsonToUser(data);
    user.username = getUsername();
    this.setState({ user });
  };

  handleChange = ({ currentTarget: input }) => {
    debugger;
    const user = { ...this.state.user };
    user[input.id] = input.value;
    this.setState({ user });
  };

  handleSubmit = async (e) => {
    e.preventDefault();

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

export default RouteProtector(Profile);
