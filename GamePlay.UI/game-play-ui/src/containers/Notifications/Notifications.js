import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import UserList from "../../components/user/UserList/UserList";

const Notifications = () => {
  const [subscribers, setSubscribers] = useState([]);
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();

    const getUsers = async () => {
      try {
        const response = await axiosPrivate.get("/user/showNotifications", {
          signal: controller.signal,
        });
        console.log(response.data);
        isMounted && setSubscribers(response.data.result);
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
      <UserList header={"Notifications"} users={subscribers} relations={Array(subscribers.length).fill(2)}/>
    </>
  );
}

export default Notifications