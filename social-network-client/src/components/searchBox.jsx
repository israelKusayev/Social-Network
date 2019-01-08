import React from 'react';

const SearchBox = ({ value, onChange }) => {
  return (
    <input
      type="text"
      id="query"
      className="form-control  mr-sm-2"
      placeholder="Search..."
      value={value}
      onChange={(e) => onChange(e.currentTarget.value)}
    />
  );
};

export default SearchBox;
