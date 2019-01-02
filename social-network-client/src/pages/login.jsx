import React, { Component } from 'react';
import Joi from 'joi';
import { Link } from 'react-router-dom';
import { LoginSchema as schema } from '../validations/joiSchemas';
import { Login as LoginUser } from '../services/authService';

export default class Login extends Component {
  state = {
    data: {
      username: '',
      password: ''
    },
    error: ''
  };

  handleChange = ({ currentTarget: input }) => {
    const data = { ...this.state.data };
    data[input.id] = input.value;
    this.setState({ data });
  };

  handleSubmit = (e) => {
    e.preventDefault();

    const { error } = Joi.validate({ ...this.state.data }, schema);
    if (error) {
      this.setState({ error: error.details[0].message });
      return;
    }
    this.setState({ error: '' });
    const data = JSON.stringify({ ...this.state.data });
    LoginUser(data);
  };

  render() {
    return (
      <>
        <h1 className="text-center">Login</h1>
        <form onSubmit={this.handleSubmit}>
          <div className="form-group">
            <label htmlFor="username">Username</label>
            <input
              value={this.state.username}
              onChange={this.handleChange}
              className="form-control"
              type="text"
              id="username"
              placeholder="username"
            />
          </div>
          <div className="form-group">
            <label htmlFor="password">Password</label>
            <input
              type="password"
              className="form-control"
              id="password"
              placeholder="Password"
              value={this.state.password}
              onChange={this.handleChange}
            />
          </div>
          {this.state.error && <div className="alert alert-danger">{this.state.error}</div>}

          <button type="submit" className="btn btn-dark">
            Login
          </button>
          <div className="float-right text-right">
            <Link to="/reset-password">reset password</Link>
          </div>
        </form>
      </>
    );
  }
}
