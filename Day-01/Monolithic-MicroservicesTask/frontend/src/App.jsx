import { useState, useEffect } from "react";

const API_BASE_URL = "http://localhost:8000/api";

const App = () => {
  const [names, setNames] = useState([]);
  const [newName, setNewName] = useState("");

  const fetchNames = async () => {
    try {
      const response = await fetch(`${API_BASE_URL}/data`);
      if (!response.ok) throw new Error("Network response was not ok");
      const data = await response.json();
      setNames(data);
    } catch (error) {
      console.error("Error fetching names:", error);
    }
  };

  const addName = async (name) => {
    try {
      const response = await fetch(`${API_BASE_URL}/data`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ name }),
      });
      if (!response.ok) throw new Error("Network response was not ok");
      const newName = await response.json();
      setNames((prevNames) => [...prevNames, newName]);
      setNewName("");
    } catch (error) {
      console.error("Error adding name:", error);
    }
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    if (newName.trim()) {
      addName(newName.trim());
    }
  };

  useEffect(() => {
    fetchNames();
  }, []);

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-100 via-gray-200 to-gray-300 flex flex-col items-center p-8">
      <h1 className="text-4xl font-extrabold text-gray-800 mb-6 bg-gradient-to-r from-teal-500 via-teal-600 to-teal-700 text-transparent bg-clip-text">
        Name List Application
      </h1>
      <div className="w-full max-w-lg bg-white shadow-2xl rounded-xl p-8 mb-6 border border-gray-200">
        <form className="flex flex-col space-y-6" onSubmit={handleSubmit}>
          <input
            className="w-full p-4 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-teal-500 transition ease-in-out duration-300"
            type="text"
            value={newName}
            onChange={(e) => setNewName(e.target.value)}
            placeholder="Enter a name"
            required
          />
          <button
            className="w-full bg-teal-500 text-white py-3 rounded-lg shadow-md hover:bg-teal-600 focus:outline-none focus:ring-4 focus:ring-teal-300 transition ease-in-out duration-300"
            type="submit"
          >
            Add Name
          </button>
        </form>
      </div>
      <div className="w-full max-w-lg">
        <h2 className="text-3xl font-semibold text-gray-700 mb-4">Names</h2>
        <ul className="space-y-4">
          {names.map((name) => (
            <li
              key={name.id}
              className="bg-white p-4 border border-gray-200 rounded-lg shadow-md hover:bg-gray-50 transition ease-in-out duration-300"
            >
              {name.name}
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default App;
