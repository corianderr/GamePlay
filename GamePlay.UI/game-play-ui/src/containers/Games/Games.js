import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation } from "react-router-dom";
import GameList from "../../components/game/GameList/GameList";
import useAuth from "hooks/useAuth";
import { Col, FormGroup, FormLabel, Modal, Row } from "react-bootstrap";
import AddGameForm from "components/game/AddGameForm/AddGameForm";
import { useTranslation } from "react-i18next";
import {
  Input,
  InputAdornment,
  InputLabel,
  TableSortLabel,
  TextField,
} from "@mui/material";

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
  const [sort, setSort] = useState({
    name: "asc",
    rating: "asc",
    players: "asc",
    time: "asc",
  });

  const handleClose = () => {
    setShow(false);
  };

  const handleShow = () => {
    setShow(true);
  };

  const handleFilter = async () => {
    const filteredGamesCopy = games.filter((game) => {
      if (filter.name && !game.name.includes(filter.name)) {
        return false;
      }
      if (filter.rating && game.averageRating < filter.rating) {
        return false;
      }
      if (
        filter.minPlayers &&
        (game.maxPlayers < filter.minPlayers ||
          game.minPlayers > filter.minPlayers)
      ) {
        return false;
      }
      if (
        filter.maxPlayers &&
        (game.maxPlayers > filter.maxPlayers ||
          game.minPlayers > filter.maxPlayers)
      ) {
        return false;
      }
      if (
        filter.minPlayTime &&
        (game.maxPlayTime < filter.minPlayTime ||
          game.minPlayTime > filter.minPlayTime)
      ) {
        return false;
      }
      if (
        filter.maxPlayTime &&
        (game.maxPlayTime > filter.maxPlayTime ||
          game.minPlayTime > filter.maxPlayTime)
      ) {
        return false;
      }
      return true;
    });
    console.log(filteredGamesCopy);
    setFilteredGames(filteredGamesCopy);
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

  const sortByPropertyName = (propertyName, sortOrder) => (a, b) => {
    const compareResult =
      sortOrder === "asc"
        ? a[propertyName] < b[propertyName]
          ? -1
          : 1
        : b[propertyName] < a[propertyName]
        ? -1
        : 1;
    return compareResult;
  };

  const onChangeSort = (name, value) => {
    const sortOrder = value === "asc" ? "desc" : "asc";

    const sortingFunctions = {
      name: sortByPropertyName("name", sortOrder),
      rating: sortByPropertyName("averageRating", sortOrder),
      players: sortByPropertyName("minPlayers", sortOrder),
      time: sortByPropertyName("minPlayTime", sortOrder),
    };

    const sortedGames = [...filteredGames].sort(sortingFunctions[name]);

    setFilteredGames(sortedGames);
    setSort((prev) => ({ ...prev, [name]: sortOrder }));
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

      <Row className="align-items-center g-3 mb-3">
        <Col md={3}>
          <FormGroup>
            <TableSortLabel
              className="d-block"
              direction={sort.name}
              onClick={() => onChangeSort("name", sort.name)}
            >
              {t("forms.name")}
            </TableSortLabel>
            <TextField
              size="small"
              color="primary"
              id="name"
              name="name"
              value={filter.name}
              placeholder="-"
              onChange={onChangeFilter}
              style={{ font: "inherit" }}
            />
          </FormGroup>
        </Col>
        <Col md={3}>
          <FormGroup>
            <TableSortLabel
              direction={sort.rating}
              onClick={() => onChangeSort("rating", sort.rating)}
            >
              {t("game.averageRating")}
            </TableSortLabel>
            <TextField
              size="small"
              id="rating"
              name="rating"
              placeholder="-"
              type="number"
              onChange={onChangeFilter}
            />
          </FormGroup>
        </Col>
        <Col md={3}>
          <FormGroup>
            <TableSortLabel
              direction={sort.players}
              onClick={() => onChangeSort("players", sort.players)}
            >
              {t("game.playersNumber")}
            </TableSortLabel>
            <TextField
              size="small"
              id="playersNumberFrom"
              name="minPlayers"
              placeholder="-"
              type="number"
              onChange={onChangeFilter}
            />
          </FormGroup>
        </Col>
        <Col md={3}>
          <FormGroup>
            <TableSortLabel
              direction={sort.time}
              onClick={() => onChangeSort("time", sort.time)}
            >
              {t("game.playTime")}
            </TableSortLabel>
            <TextField
              size="small"
              id="playTimeFrom"
              name="minPlayTime"
              placeholder="-"
              type="number"
              onChange={onChangeFilter}
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    {t("game.min")}
                  </InputAdornment>
                ),
              }}
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
