import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import GameList from "../../components/game/GameList/GameList";
import useAuth from "hooks/useAuth";
import { Col, FormGroup, FormLabel, Modal, Row } from "react-bootstrap";
import AddGameForm from "components/game/AddGameForm/AddGameForm";
import { useTranslation } from "react-i18next";
import { Input } from "@mui/material";

const Games = () => {
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();

  const { auth } = useAuth();
  const { t } = useTranslation();

  const [games, setGames] = useState([]);
  const [filteredGames, setFilteredGames] = useState([]);
  const [show, setShow] = useState(false);
  const [filter, setFilter] = useState({
    name: "",
    rating: "",
    maxPlayers: "",
    minPlayers: "",
    maxPlayTime: "",
    minPlayTime: "",
  });

  const handleClose = () => {
    setShow(false);
  };

  const handleShow = () => {
    setShow(true);
  };

  const handleFilter = async () => {
    let tempGames = games;
    console.log(filter);

    console.log(tempGames);
    if (filter.name !== "") {
      tempGames = tempGames.filter((g) => g.name.includes(filter.name));
    }
    if (filter.rating !== "") {
      tempGames = tempGames.filter((g) => g.averageRating >= filter.rating);
    }
    if (filter.maxPlayers !== "") {
      tempGames = tempGames.filter((g) => g.maxPlayers >= filter.maxPlayers && g.minPlayers <= filter.maxPlayers);
    }
    if (filter.minPlayers !== "") {
      tempGames = tempGames.filter((g) => g.maxPlayers >= filter.minPlayers && g.minPlayers <= filter.minPlayers);
    }
    if (filter.maxPlayTime !== "") {
      tempGames = tempGames.filter((g) => g.minPlayTime <= filter.maxPlayTime && g.maxPlayTime >= filter.maxPlayTime);
    }
    if (filter.minPlayTime !== "") {
      tempGames = tempGames.filter((g) => g.minPlayTime <= filter.minPlayTime && g.maxPlayTime >= filter.minPlayTime);
    }
    setFilteredGames(tempGames);
  };

  const getGames = async () => {
    try {
      const response = await axiosPrivate.get("/game");
      setGames(response.data.result);
      setFilteredGames(response.data.result);
    } catch (err) {
      if (err.name === "CanceledError") {
        return;
      }

      console.error(err);
      navigate("/login", { state: { from: location }, replace: true });
    }
  };

  useEffect(() => {
    getGames();
  }, []);

  useEffect(() => {
    handleFilter();
  }, [filter]);

  const onChangeFilter = (e) => {
    const { name, value } = e.target;
    setFilter((prev) => ({ ...prev, [name]: value }));
  };

  return (
    <>
      {auth?.accessToken !== undefined && auth?.roles.includes("admin") && (
        <button
          className="btn btn-primary btn-sm opacity-75 w-25"
          onClick={handleShow}
        >
          {t("forms.add")}
        </button>
      )}

      <Row className="align-items-center g-3 my-3">
        <Col md={2}>
          <FormGroup>
            <Input
              id="name"
              name="name"
              value={filter.name}
              type="text"
              placeholder="Name"
              onChange={onChangeFilter}
              style={{ fontSize: "inherit" }}
            />
          </FormGroup>
        </Col>
        <Col md={2}>
          <FormGroup>
            <Input
              id="rating"
              name="rating"
              placeholder="Rating From"
              type="number"
              onChange={onChangeFilter}
            />
          </FormGroup>
        </Col>
        <Col md={3}>
          <FormGroup>
            <Input
              id="playersNumberFrom"
              name="minPlayers"
              placeholder="Players Number From"
              type="number"
              onChange={onChangeFilter}
            />
          </FormGroup>
        </Col>
        <Col md={1}>
          <FormGroup>
            <Input
              id="playersNumberTo"
              name="maxPlayers"
              placeholder="To"
              type="number"
              onChange={onChangeFilter}
            />
          </FormGroup>
        </Col>
        <Col md={2}>
          <FormGroup>
            <Input
              id="playTimeFrom"
              name="minPlayTime"
              placeholder="Play Time From"
              type="number"
              onChange={onChangeFilter}
            />
          </FormGroup>
        </Col>
        <Col md={2}>
          <FormGroup>
            <Input
              id="playTimeTo"
              name="maxPlayTime"
              placeholder="To"
              type="number"
              onChange={onChangeFilter}
            />
          </FormGroup>
        </Col>
      </Row>

      <GameList
        games={filteredGames}
        header={t("navMenu.games")}
        refreshGames={getGames}
      />
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>
            {t("forms.add")} {t("game.what")}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <AddGameForm handleClose={handleClose} updateData={getGames} />
        </Modal.Body>
      </Modal>
    </>
  );
};

export default Games;
