import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation, Link, useParams } from "react-router-dom";
import GameList from "../../components/game/GameList/GameList";

const CollectionDetails = () => {
  const [collection, setCollection] = useState({ games: [], name: "" });
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();

  const { collectionId } = useParams();

  useEffect(() => {
    getGames();
  }, []);

  const getGames = async () => {
    try {
      const response = await axiosPrivate.get(
        `/collection/getById/${collectionId}`
      );
      setCollection(response.data.result);
    } catch (err) {
      if (err.name === "CanceledError") {
        return;
      }
      console.error(err);
      navigate("/login", { state: { from: location }, replace: true });
    }
  };

  return (
    <GameList
      games={collection.games}
      header={collection.name}
      collectionId={collectionId}
      refreshGames={getGames}
    />
  );
};

export default CollectionDetails;
