import React from 'react';
import { getJwt } from '../services/jwtService';

function NotFound(props) {
  return (
    <div className="text-center">
      <h1 className="font-weight-bold mb-3">Page not found!</h1>
      {getJwt() ? (
        <input
          type="button"
          className="btn btn-dark"
          value="Return to home page"
          onClick={() => props.history.replace('/')}
        />
      ) : (
        <input
          type="button"
          className="btn btn-dark"
          value="Return to login page"
          onClick={() => props.history.replace('/login')}
        />
      )}
    </div>
  );
}

export default NotFound;
