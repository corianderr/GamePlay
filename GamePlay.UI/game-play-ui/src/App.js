import Register from "./components/auth/Register/Register";
import Login from "./components/auth/Login/Login";
import Users from "./containers/Users/Users";
import Unauthorized from "./components/auth/Unauthorized/Unauthorized";
import RequireAuth from "./components/auth/RequireAuth/RequireAuth";
import { Routes, Route } from "react-router-dom";
import Layout from "./components/Layout";
import PersistLogin from "./components/auth/PersistLogin/PersistLogin";
import UserDetails from "./containers/UserDetails/UserDetails";
import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faBell,
  faSquarePlus,
  faTrash,
  faSquarePollVertical,
  faPenToSquare,
  faArrowsRotate,
} from "@fortawesome/free-solid-svg-icons";
import Notifications from "containers/Notifications/Notifications";
import Friends from "containers/Friends/Friends";
import Followers from "containers/Followers/Followers";
import CollectionDetails from "containers/CollectionDetails/CollectionDetails";
import GameDetails from "containers/GameDetails/GameDetails";
import Games from "containers/Games/Games";
import Rounds from "containers/Rounds/Rounds";
import GameRounds from "containers/GameRounds/GameRounds";
import RoundDetails from "containers/RoundDetails/RoundDetails";
import Players from "containers/Players/Players";

const ROLES = {
  User: "user",
  Admin: "admin",
};

function App() {
  library.add(
    faBell,
    faSquarePlus,
    faTrash,
    faSquarePollVertical,
    faPenToSquare,
    faArrowsRotate
  );

  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        <Route path="unauthorized" element={<Unauthorized />} />
        <Route path="/games" element={<Games />} />
        <Route path="/" element={<Games />} />
        <Route path="/gameDetails/:gameId" element={<GameDetails />} />

        <Route element={<PersistLogin />}>
          <Route
            element={<RequireAuth allowedRoles={[ROLES.User, ROLES.Admin]} />}
          >
            <Route path="/users" element={<Users />} />
            <Route path="/notifications" element={<Notifications />} />
            <Route path="/followers/:userId" element={<Followers />} />
            <Route path="/friends/:userId" element={<Friends />} />
            <Route path="/userDetails/:userId" element={<UserDetails />} />
            <Route path="/roundDetails/:roundId" element={<RoundDetails />} />
            <Route
              path="/collectionDetails/:collectionId"
              element={<CollectionDetails />}
            />
            <Route path="/rounds" element={<Rounds />} />
            <Route
              path="/gameRounds/:gameId/:gameName"
              element={<GameRounds />}
            />
          </Route>
          <Route
            element={<RequireAuth allowedRoles={[ROLES.Admin]} />}
          >
            <Route path="/players" element={<Players />} />
          </Route>
        </Route>
      </Route>
    </Routes>
  );
}

export default App;
