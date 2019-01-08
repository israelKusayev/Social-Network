import React, { Component } from 'react';
import ProfileTemplate from './profileTemplate';

export default class UserProfile extends Component {
  state = {
    user: {}
  };
  componentDidMount = () => {
    this.setState({
      user: {
        username: 'israel',
        firstName: 'alla',
        lastName: 'odposd',
        age: 18,
        workPlace: 'sela'
      }
    });
  };

  render() {
    const { user } = this.state;
    console.log(user);

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
