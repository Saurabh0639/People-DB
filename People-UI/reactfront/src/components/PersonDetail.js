import React from "react";
import { Link } from "react-router-dom";
import user from "../images/user.png";

const PersonDetail = (props) => {
  const { firstName, lastName, age } = props.location.state.person;
  return (
    <div className="main">
      <div className="ui card centered">
        <div className="image">
          <img src={user} alt="user" />
        </div>
        <div className="content">
          <div className="header">Full Name: {firstName} {lastName}</div>
          <div className="description">Age: {age}</div>

        </div>
      </div>
      <div className="center-div">
        <Link to="/">
          <button className="ui blue button center">Back to People List</button>
        </Link>
      </div>
    </div>
  );
};

export default PersonDetail;
