import Register from "./components/auth/Register/Register";
import Login from "./components/auth/Login/Login";
import Home from "./components/Home";
import Users from "./containers/Users/Users";
import Unauthorized from "./components/auth/Unauthorized/Unauthorized";
import RequireAuth from "./components/auth/RequireAuth/RequireAuth";
import { Routes, Route } from "react-router-dom";
import Layout from "./components/Layout";
import PersistLogin from "./components/auth/PersistLogin/PersistLogin";
import UserDetails from "./containers/UserDetails/UserDetails";
import { library } from '@fortawesome/fontawesome-svg-core'
import { faBell, faSquarePlus, faTrash, faSquarePollVertical } from '@fortawesome/free-solid-svg-icons';
import Notifications from "containers/Notifications/Notifications";
import Friends from "containers/Friends/Friends";
import Followers from "containers/Followers/Followers";
import CollectionDetails from "containers/CollectionDetails/CollectionDetails";
import GameDetails from "containers/GameDetails/GameDetails";
import Games from "containers/Games/Games";
import GameRounds from "components/game/GameRounds/GameRounds";


const ROLES = {
  User: "user",
  Admin: "admin",
};

function App() {
  library.add(faBell, faSquarePlus, faTrash, faSquarePollVertical);

  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        <Route path="unauthorized" element={<Unauthorized />} />
        <Route path="/" element={<Home />} />
        <Route path="/games" element={<Games />} />

        <Route element={<PersistLogin />}>
          <Route element={<RequireAuth allowedRoles={[ROLES.User, ROLES.Admin]}/>}>
            <Route path="/users" element={<Users />} />
            <Route path="/notifications" element={<Notifications />} />
            <Route path="/followers/:userId" element={<Followers />} />
            <Route path="/friends/:userId" element={<Friends />} />
            <Route path="/userDetails/:userId" element={<UserDetails />} />
            <Route path="/gameDetails/:gameId" element={<GameDetails />} />
            <Route path="/collectionDetails/:collectionId" element={<CollectionDetails />} />
            <Route path="/gameRounds" element={<GameRounds />} />
          </Route>
        </Route>
      </Route>
    </Routes>
  );
}

export default App;
