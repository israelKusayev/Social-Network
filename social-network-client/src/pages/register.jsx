import React, { Component } from 'react';
import Joi from 'joi';
import { RegisterSchema as schema } from '../validations/joiSchemas';
import { Register as RegisterUser } from '../services/authService';

export default class Register extends Component {
  state = {
    data: {
      username: '',
      email: '',
      password: '',
      password2: ''
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

    //validate
    const { error } = Joi.validate({ ...this.state.data }, schema);
    if (error) {
      this.setState({ error: error.details[0].message });
      return;
    }
    this.setState({ error: '' });

    //register
    const data = JSON.stringify({
      username: this.state.data.username,
      email: this.state.data.email,
      password: this.state.data.password
    });
    RegisterUser(data);

    // todo if error else redirect
  };

  render() {
    return (
      <>
        <h1 className="text-center">Register</h1>
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
          <div className="form-group">
            <label htmlFor="password2">Confirm password</label>
            <input
              type="password"
              className="form-control"
              id="password2"
              placeholder="Confirm password"
              value={this.state.password2}
              onChange={this.handleChange}
            />
          </div>
          {this.state.error && <div className="alert alert-danger">{this.state.error}</div>}
          <button type="submit" className="btn btn-dark">
            Register
          </button>
        </form>
      </>
    );
  }
}
