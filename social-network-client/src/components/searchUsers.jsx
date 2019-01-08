import React, { Component } from 'react';
import SearchBox from './searchBox';
import { Get } from '../services/httpService';

class SearchUsers extends Component {
  state = {
    searchQuery: '',
    users: []
  };
  socialUrl = process.env.REACT_APP_SOCIAL_URL;
  timer;

  handleChange = async (searchQuery) => {
    this.setState({ searchQuery });

    if (!searchQuery) clearTimeout(this.timer);
    else {
      clearTimeout(this.timer);

      this.timer = setTimeout(async () => {
        const res = await Get(this.socialUrl + 'users/getUsers/' + this.state.searchQuery);
        const users = await res.json();
        this.setState({ users });
      }, 1000);
    }
  };

  render() {
    return (
      <>
        <SearchBox onChange={this.handleChange} value={this.state.searchQuery} />
        <div class="dropdown">
          <button class="dropbtn">Dropdown</button>
          <div class="dropdown-content">
            <a href="#">Link 1</a>
            <a href="#">Link 2</a>
            <a href="#">Link 3</a>
          </div>
        </div>
      </>
    );
  }
}
export default SearchUsers;
