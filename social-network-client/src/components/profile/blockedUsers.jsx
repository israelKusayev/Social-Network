import React, { Component } from 'react';
import FollowerTab from '../followerTab';
import { getBlockedUsers, unblockUser } from '../../services/usersService';
import { toast } from 'react-toastify';

class BlockedUsers extends Component {
  state = {
    blockedUsers: []
  };

  componentDidMount = async () => {
    const res = await getBlockedUsers();
    if (res.status === 200) {
      const blockedUsers = await res.json();
      this.setState({ blockedUsers });
    } else {
      toast.error('something went wrong...');
    }
  };

  unblock = async (user) => {
    const res = await unblockUser(user.userId);
    if (res.status === 200) {
      toast.success(user.username + ' unblocked successfully !');

      const blockedUsers = [...this.state.blockedUsers];
      const index = blockedUsers.indexOf(user);
      blockedUsers.splice(index, 1);
      this.setState({ blockedUsers });
    } else {
      toast.error('unblocked faild, try again...');
    }
  };

  render() {
    const { blockedUsers } = this.state;
    return (
      <div>
        <h1>Blocked user</h1>
        {blockedUsers.length !== 0 ? (
          blockedUsers.map((user) => {
            return (
              <FollowerTab
                key={user.userId}
                rightBtnName={'Unblock'}
                onRightBtnClicked={() => this.unblock(user)}
                name={user.username}
              />
            );
          })
        ) : (
          <h2 className="text-danger">You have blocked no users</h2>
        )}
      </div>
    );
  }
}
export default BlockedUsers;
