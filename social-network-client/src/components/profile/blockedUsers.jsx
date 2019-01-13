import React, { Component } from 'react';
import FollowerTab from '../followerTab';

class BlockedUsers extends Component {
  state = {
    data: [{ username: 'eli aliho' }, { username: 'oleg olegov' }, { username: 'avi cohen' }]
  };

  unblock = () => {};

  blockUser = () => {};
  render() {
    const { data } = this.state;
    return (
      <div>
        <h1>Followers</h1>
        {data.map((user) => {
          return <FollowerTab rightBtnName={'Unblock'} onRightBtnClicked={this.unblock} name={user.username} />;
        })}
      </div>
    );
  }
}
export default BlockedUsers;
