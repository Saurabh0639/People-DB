import React, { useState, useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import axios from "axios";
import { v4 as uuid_v4 } from "uuid";

const User = () => {
  const [user, setUser] = useState({
    firstName: "",
    lastName: "",
    age: ""
  });
  const { id } = useParams();
  useEffect(() => {
    loadUser();
  }, []);
  const loadUser = async () => {
    const res = await axios.get(`http://localhost:5120/api/persons/${id}`);
    setUser(res.data);
  };
  return (
    <div className="container py-4">
      <Link className="btn btn-primary" to="/">
        back to Home
      </Link>
      <h1 className="display-4">User Id: {id}</h1>
      <hr />
      <ul className="list-group w-50">
        <li className="list-group-item">name: {user.firstName}</li>
        <li className="list-group-item">user name: {user.lastName}</li>
        <li className="list-group-item">phone: {user.age}</li>
      </ul>
    </div>
  );
};

export default User;
