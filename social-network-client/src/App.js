import React, { Component } from 'react';
import { Switch, Redirect, Route } from 'react-router-dom';
import Register from './pages/register';
import Login from './pages/login';
import ResetPassword from './pages/resetPassword';

import './App.css';
import Navbar from './components/navbar';

class App extends Component {
  render() {
    return (
      <>
        <Navbar />
        <div className="container">
          <Switch>
            <Route path="/register" component={Register} />
            <Route path="/login" component={Login} />
            <Route path="/reset-password" component={ResetPassword} />
            <Redirect from="/" to="/register" />
          </Switch>
        </div>
      </>
    );
  }
}

export default App;
