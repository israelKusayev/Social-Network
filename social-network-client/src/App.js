import React, { Component } from 'react';
import { Switch, Redirect, Route } from 'react-router-dom';
import Register from './pages/register';
import Login from './pages/login';
import Feed from './pages/feed';
import ResetPassword from './pages/resetPassword';

import './App.css';
import './styles/customButtons.css';
import Navbar from './components/navbar/navbar';
import NotFound from './components/notFound';
import Profile from './pages/profile';
import Notifications from './pages/notifications';
import CreatePost from './pages/createPost';
import Followers from './components/profile/followers';

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

            <Route path="/feed" component={Feed} />
            <Route path="/create-post" component={CreatePost} />
            <Route path="/notifications" component={Notifications} />
            <Route path="/profile/followers" component={Followers} />
            <Route path="/profile" component={Profile} />

            <Route path="/not-found" component={NotFound} />
            <Route path="/" exact={true} component={Feed} />
            <Redirect to="/not-found" />
          </Switch>
        </div>
      </>
    );
  }
}

export default App;
