import Register from "./components/auth/Register";
import Login from "./components/auth/Login";
import Home from "./components/Home";
import Users from "./components/user/Users";
import Unauthorized from "./components/auth/Unauthorized";
import RequireAuth from "./components/auth/RequireAuth";
import { Routes, Route } from "react-router-dom";
import Layout from "./components/Layout";
import PersistLogin from "./components/auth/PersistLogin";
import UserDetails from "./components/user/UserDetails";

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
            <Route path="/userDetails/:userId" element={<UserDetails />} />
          </Route>
        </Route>
      </Route>
    </Routes>
  );
}

export default App;
