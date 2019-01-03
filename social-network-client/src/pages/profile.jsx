import React, { Component } from 'react';
import User from '../models/user';
import { Put, Get } from '../services/httpService';
import { getJwt } from '../services/authService';

export default class Profile extends Component {
  state = {
    user: new User()
  };
  identityUrl = process.env.REACT_APP_IDENTITY_URL;

  handleChange = ({ currentTarget: input }) => {
    const user = { ...this.state.user };
    user[input.id] = input.value;
    this.setState({ user });
  };
  componentDidMount = async () => {
    const res = await Get(
      `${this.identityUrl}UsersIdentity/c959d9a6-f976-4013-88cc-79ca62f6deb0
    `,
      getJwt()
    );
    var data = await res.json();
    let user = new User();
    user = { ...JSON.parse(data) };
    console.log(user);

    this.setState({ user });
  };

  handleSubmit = () => {};

  render() {
    const { user } = this.state;
    return (
      <div className="container mt-1">
        <div className="row">
          <div className="col-md-1 ">
            <div className="list-group " />
          </div>
          <div className="col-md-10">
            <div className="card bg-dark text-white">
              <div className="card-body">
                <div className="row">
                  <div className="col-md-12">
                    <h4>Your Profile</h4>
                    <hr />
                  </div>
                </div>
                <div className="row">
                  <div className="col-md-12">
                    <form onSubmit={this.handleSubmit}>
                      <div className="form-group row">
                        <label htmlFor="username" className="col-4 col-form-label">
                          User Name
                        </label>
                        <div className="col-8">
                          <input
                            id="username"
                            name="username"
                            className="form-control here"
                            required="required"
                            type="text"
                            readOnly={true}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label htmlFor="firstName" className="col-4 col-form-label">
                          First Name
                        </label>
                        <div className="col-8">
                          <input
                            id="firstName"
                            name="firstName"
                            placeholder="First Name"
                            className="form-control here"
                            type="text"
                            value={user.firstName}
                            onChange={this.handleChange}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label htmlFor="lastname" className="col-4 col-form-label">
                          Last Name
                        </label>
                        <div className="col-8">
                          <input
                            id="lastname"
                            name="lastname"
                            placeholder="Last Name"
                            className="form-control here"
                            type="text"
                            value={user.lastname}
                            onChange={this.handleChange}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label htmlFor="address" className="col-4 col-form-label">
                          Address
                        </label>
                        <div className="col-8">
                          <input
                            id="address"
                            name="address"
                            placeholder="Address"
                            className="form-control here"
                            required="required"
                            type="text"
                            value={user.address}
                            onChange={this.handleChange}
                          />
                        </div>
                      </div>

                      <div className="form-group row">
                        <label htmlFor="email" className="col-4 col-form-label">
                          Email
                        </label>
                        <div className="col-8">
                          <input
                            id="email"
                            name="email"
                            placeholder="Email"
                            className="form-control here"
                            required="required"
                            type="email"
                            value={user.email}
                            onChange={this.handleChange}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label htmlFor="age" className="col-4 col-form-label">
                          Age
                        </label>
                        <div className="col-8">
                          <input
                            id="age"
                            name="age"
                            placeholder="Age"
                            className="form-control here"
                            type="number"
                            value={user.age}
                            onChange={this.handleChange}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label htmlFor="bio" className="col-4 col-form-label">
                          Bio
                        </label>
                        <div className="col-8">
                          <textarea
                            id="bio"
                            name="bio"
                            placeholder="Bio"
                            cols="40"
                            rows="4"
                            className="form-control"
                            value={user.bio}
                            onChange={this.handleChange}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label htmlFor="workPlace" className="col-4 col-form-label">
                          Work place
                        </label>
                        <div className="col-8">
                          <input
                            id="workPlace"
                            name="workPlace"
                            placeholder="Work place"
                            className="form-control here"
                            type="text"
                            value={user.workPlace}
                            onChange={this.handleChange}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <div className="offset-4 col-8">
                          <button name="submit" type="submit" className="btn btn-pink">
                            Update My Profile
                          </button>
                        </div>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}
