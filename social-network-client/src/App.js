import React, { Component } from 'react';
import { Switch, Redirect, Route } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import Navbar from './components/navbar/navbar';

import Register from './pages/register';
import Login from './pages/login';
import ResetPassword from './pages/resetPassword';

import Feed from './pages/feed';
import NotFound from './components/notFound';
import Profile from './pages/profile';
import Notifications from './pages/notifications';
import CreatePost from './pages/createPost';
import Post from './pages/post';

import './App.css';
import './styles/customButtons.css';
import './styles/autoComplete.css';
import './styles/pageUp.css';
import 'react-toastify/dist/ReactToastify.css';
import UserProfile from './components/userProfile';
import './services/xmppService';

class App extends Component {
  state = { pageUp: false };

  componentDidMount = () => {
    window.onscroll = () => {
      if (window.pageYOffset <= 0) {
        this.setState({ pageUp: false });
      } else if (!this.state.pageUp) {
        this.setState({ pageUp: true });
      }
    };
  };
  onPageUp = () => {
    window.scrollTo(0, 0);
  };

  render() {
    return (
      <>
        <ToastContainer />
        <Navbar />
        <div className="container">
          <Switch>
            <Route path="/register" component={Register} />
            <Route path="/login" component={Login} />
            <Route path="/reset-password" component={ResetPassword} />

            <Route path="/feed" component={Feed} />
            <Route path="/create-post" component={CreatePost} />
            <Route path="/notifications" component={Notifications} />
            <Route path="/profile/:id" component={UserProfile} />
            <Route path="/profile" component={Profile} />
            <Route path="/post/:postId" component={Post} />
            <Route path="/comment/:postId" component={Post} />

            <Route path="/not-found" component={NotFound} />
            <Route path="/" exact={true} component={Feed} />
            <Redirect to="/not-found" />
          </Switch>
          {this.state.pageUp && (
            <div onClick={this.onPageUp} className="pageUp">
              <i className="fa fa-chevron-circle-up" />
            </div>
          )}
        </div>
      </>
    );
  }
}

export default App;
