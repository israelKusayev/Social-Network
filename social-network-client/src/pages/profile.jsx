import React, { Component } from 'react';
import RouteProtector from '../HOC/routeProtector';
import ListGroup from '../components/listGroup';
import Followers from '../components/profile/followers';
import Following from '../components/profile/following';
import BlockedUsers from '../components/profile/blockedUsers';
import MyProfile from '../components/profile/myProfile';
import { deleteJwt } from '../services/jwtService';

class Profile extends Component {
  state = {
    selected: 'MyProfile'
  };
  handleSelect = ({ name }) => {
    if (name === 'Logout') {
      deleteJwt();
      this.props.history.replace('/login');
    }
    this.setState({ selected: name });
  };

  render() {
    const items = [
      {
        id: '1',
        name: 'MyProfile',
        label: 'My profile',
        payload: <MyProfile />
      },
      { id: '2', name: 'Followers', payload: <Followers /> },
      { id: '3', name: 'Following', payload: <Following /> },
      { id: '4', name: 'BlockedUsers', label: 'Blocked users', payload: <BlockedUsers /> },
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
