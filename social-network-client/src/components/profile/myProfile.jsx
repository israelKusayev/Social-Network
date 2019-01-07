import React, { Component } from 'react';
import User from '../../models/user';
import ProfileTemplate from '../profileTemplate';
import { getJwt, getUsername, getUserId } from '../../services/jwtService';
import { Put, Get } from '../../services/httpService';
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

    const user = JSON.stringify(this.state.user);
    const res = await Put(`${this.identityUrl}UsersIdentity`, user, getJwt());

    if (res.status !== 200) {
      this.getUser();
    }
  };

  render() {
    const { user } = this.state;

    return (
      <ProfileTemplate
        title={'Your profile'}
        onChange={this.handleChange}
        isReadOnly={false}
        updateProfile={this.handleSubmit}
        user={user}
      />
    );
  }
}
