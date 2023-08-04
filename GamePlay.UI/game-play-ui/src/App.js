import Register from "./components/Register";
import Login from "./components/Login";
import Home from "./components/Home";
import Users from "./components/Users";
import Unauthorized from "./components/Unauthorized";
import RequireAuth from "./components/RequireAuth";
import { Routes, Route } from "react-router-dom";
import Layout from "./components/Layout";
import PersistLogin from "./components/PersistLogin";

const ROLES = {
  User: "user",
  Admin: "admin",
};

function App() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        <Route path="unauthorized" element={<Unauthorized />} />
        <Route path="/" element={<Home />} />

        <Route element={<PersistLogin />}>
          <Route element={<RequireAuth allowedRoles={[ROLES.User, ROLES.Admin]}/>}>
            <Route path="/users" element={<Users />} />
          </Route>
        </Route>
      </Route>
    </Routes>
  );
}

export default App;
