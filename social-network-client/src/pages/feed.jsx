import React, { Component } from 'react';
import RouteProtector from '../HOC/routeProtector';

class Feed extends Component {
  render() {
    return (
      <div>
        <h1>feed</h1>
      </div>
    );
  }
}

export default RouteProtector(Feed);
