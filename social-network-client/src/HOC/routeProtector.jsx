import React from 'react';
import { Redirect } from 'react-router-dom';
import { getJwt } from '../services/authService';

const RouteProtector = (WrappedComponent) =>
  class WithLoading extends React.Component {
    render() {
      return getJwt() ? <WrappedComponent {...this.props} /> : <Redirect to="/Register" />;
    }
  };
export default RouteProtector;