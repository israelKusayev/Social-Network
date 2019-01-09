import React, { Component } from 'react';
import ProfileTemplate from './profileTemplate';
import { convertJsonToUser } from '../converters/userConvertor';
import User from '../models/user';
import { getUser } from '../services/usersService';

export default class UserProfile extends Component {
  state = {
    user: new User()
  };

  componentDidMount = () => {
    this.getUser();
  };

  getUser = async () => {
    const res = await getUser(this.props.match.params.id);
    var data = await res.json();
    let user = convertJsonToUser(data);
    this.setState({ user });
  };

  render() {
    const { user } = this.state;

    return (
      <>
        <div className="my-3">
          <div className="row">
            <button className="offset-5 btn btn-dark text-pink">follow</button>
            <button className="offset-1 btn btn-dark text-pink">block</button>
          </div>
        </div>
        <ProfileTemplate title={'User details'} isReadOnly={true} user={user} />
      </>
    );
  }
}
