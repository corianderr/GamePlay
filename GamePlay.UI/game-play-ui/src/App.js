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
import { library } from '@fortawesome/fontawesome-svg-core'
import { faBell, faSquarePlus } from '@fortawesome/free-solid-svg-icons';
import Notifications from "components/user/Notifications";
import Followers from "components/user/Followers";


const ROLES = {
  User: "user",
  Admin: "admin",
};

function App() {
  library.add(faBell, faSquarePlus);

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
            <Route path="/notifications" element={<Notifications />} />
            <Route path="/followers/:userId" element={<Followers />} />
            <Route path="/userDetails/:userId" element={<UserDetails />} />
          </Route>
        </Route>
      </Route>
    </Routes>
  );
}

export default App;
