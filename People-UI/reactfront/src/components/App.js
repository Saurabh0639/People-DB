import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
//import { uuid } from "uuidv4";
import { v4 as uuid_v4 } from "uuid";
import api from "../api/people";
import "./App.css";
import Header from "./Header";
import AddPerson from "./AddPerson";
import PersonList from "./PersonList";
import PersonDetail from "./PersonDetail";
import EditPerson from "./EditPerson";


function App() {
  const [people, setPeople] = useState([]);

  const retrievePeople = async () => {
    const response = await api.get("/persons");
    return response.data;
  };

  const addPersonHandler = async (person) => {
    console.log(person);
    const request = {
      id: uuid_v4(),
      ...person,
    };

    const response = await api.post("/persons", request);
    console.log(response.data);
    setPeople([...people, response.data]);
  };

  const updatePersonHandler = async (person) => {
    const response = await api.put(`/persons/${person.id}`, person);
    const { id, firstName, lastName } = response.data;
    setPeople(
      people.map((person) => {
        return person.id === id ? { ...response.data } : person;
      })
    );
   // setPeople([])
  };

  const removePersonHandler = async (id) => {
    await api.delete(`/persons/${id}`);
    const newPersonList = people.filter((person) => {
      return person.id !== id;
    });

    setPeople(newPersonList);
  };

  useEffect(() => {
    const getAllPeople = async () => {
      const allPeople = await retrievePeople();
      if (allPeople) setPeople(allPeople);
    };

    getAllPeople();
  }, []);

  return (
    <div className="ui container">
      <Router>
        <Header />
        <Switch>
          <Route
            path="/"
            exact
            render={(props) => (
              <PersonList
                {...props}
                people={people}
                getPersonId={removePersonHandler}
              />
            )}
          />
          <Route
            path="/add"
            render={(props) => (
              <AddPerson {...props} addPersonHandler={addPersonHandler} />
            )}
          />

          <Route
            path="/edit"
            render={(props) => (
              <EditPerson
                {...props}
                updatePersonHandler={updatePersonHandler}
              />
            )}
          />

          <Route path="/person/:id" component={PersonDetail} />
        </Switch>
      </Router>
    </div>
  );
}

export default App;
