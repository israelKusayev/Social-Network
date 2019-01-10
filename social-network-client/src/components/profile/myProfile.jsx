import React, { Component } from 'react';
import User from '../../models/user';
import ProfileTemplate from '../profileTemplate';
import { getUsername, getUserId } from '../../services/jwtService';
import { convertJsonToUser } from '../../converters/userConvertor';
import { getUser, updateUser } from '../../services/usersService';
import { toast } from 'react-toastify';

export default class MyProfile extends Component {
  state = {
    user: new User()
  };

  componentDidMount = () => {
    this.getUser();
  };

  getUser = async () => {
    const res = await getUser(getUserId());
    if (res.status !== 200) {
      toast.error('something went wrong...');
    } else {
      const data = await res.json();
      let user = convertJsonToUser(data);
      user.username = getUsername();
      this.setState({ user });
    }
  };

  handleChange = ({ currentTarget: input }) => {
    const user = { ...this.state.user };
    user[input.id] = input.value;
    this.setState({ user });
  };

  handleSubmit = async (e) => {
    e.preventDefault();

    const user = JSON.stringify(this.state.user);
    const res = await updateUser(user);

    if (!res || res.status !== 200) {
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
