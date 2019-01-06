import React from 'react';
import { Redirect } from 'react-router-dom';
import { getJwt } from '../services/jwtService';

const RouteProtector = (WrappedComponent) =>
  class WithLoading extends React.Component {
    render() {
      //  return getJwt() ? <WrappedComponent {...this.props} /> : <WrappedComponent {...this.props} />;
      return getJwt() ? <WrappedComponent {...this.props} /> : <Redirect to="/login" />;
    }
  };
export default RouteProtector;
