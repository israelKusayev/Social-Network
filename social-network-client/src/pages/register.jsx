import React, { Component } from 'react';
import Joi from 'joi';
import { RegisterSchema as schema } from '../validations/joiSchemas';
import { register } from '../services/authService';

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

    //register
    const data = JSON.stringify({
      username: this.state.data.username,
      email: this.state.data.email,
      password: this.state.data.password
    });
    register(data)
      .then(() => {
        this.props.history.push('/');
      })
      .catch(() => {
        this.setState({ error: 'Username already exists in the database' });
      });
  };

  render() {
    const { data } = this.state;
    return (
      <>
        <h1 className="text-center">Register</h1>
        <form onSubmit={this.handleSubmit}>
          <div className="form-group">
            <label htmlFor="username">Username</label>
            <input
              value={data.username}
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
              value={data.email}
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
              value={data.password}
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
              value={data.password2}
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
