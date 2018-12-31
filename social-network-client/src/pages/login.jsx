import React, { Component } from 'react';
import Joi from 'joi';
import { Link } from 'react-router-dom';
import { LoginSchema as schema } from '../validations/joiSchemas';
export default class Login extends Component {
  state = {
    data: {
      username: '',
      email: '',
      password: ''
    },
    error: ''
  };

  handleChange = ({ currentTarget: input }) => {
    // const errors = { ...this.state.errors };
    // const errorMessage = this.validateProperty(input);
    // if (errorMessage) errors[input.name] = errorMessage;
    // else delete errors[input.name];

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
    const data = JSON.stringify({
      username: this.state.data.username,
      email: this.state.data.email,
      password: this.state.data.password
    });
    console.log(data);
    this.setState({ error: '' });
    // fetch('http://localhost:52589/api/register');
    fetch('http://localhost:52589/api/register', {
      method: 'POST',
      body: data,
      headers: {
        'Content-Type': 'application/json'
      }
    }).then((res) => console.log(res));
  };

  facebookLogin = (res) => {
    console.log(res);
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
            <label htmlFor="email">Email address</label>
            <input
              type="email"
              className="form-control"
              id="email"
              aria-describedby="emailHelp"
              placeholder="Enter email"
              value={this.state.email}
              onChange={this.handleChange}
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
