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
        <div className="mt-4">
          <ProfileTemplate title={'User details'} isReadOnly={true} user={user} />
        </div>
      </>
    );
  }
}
