import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import UserList from "../../components/user/UserList/UserList";

const Users = () => {
  const [users, setUsers] = useState([]);
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();

    const getUsers = async () => {
      try {
        const response = await axiosPrivate.get("/user", {
          signal: controller.signal,
        });
        console.log(response.data);
        isMounted && setUsers(response.data.result);
      } catch (err) {
        if (err.name === "CanceledError") {
          return;
        }

        console.error(err);
        navigate("/login", { state: { from: location }, replace: true });
      }
    };
    getUsers();

    return () => {
      isMounted = false;
      controller.abort();
    };
  }, []);

  return (
    <>
      <UserList header={"Users"} users={users} />
    </>
  );
};

export default Users;
