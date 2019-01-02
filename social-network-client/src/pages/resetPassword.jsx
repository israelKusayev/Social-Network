import React, { Component } from 'react';
import Joi from 'joi';
import { ResetPasswordSchema as schema } from '../validations/joiSchemas';

export default class ResetPassword extends Component {
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

    const { error } = Joi.validate({ ...this.state.data }, schema);
    if (error) {
      this.setState({ error: error.details[0].message });
      return;
    }
    this.setState({ error: '' });
  };
  render() {
    return (
      <>
        <h1 className="text-center">Reset password</h1>
        <form onSubmit={this.handleSubmit}>
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
            <label htmlFor="password">New password</label>
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
            <label htmlFor="password2">Confirm new password</label>
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
            Reset
          </button>
        </form>
      </>
    );
  }
}
