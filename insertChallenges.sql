INSERT INTO `bananatestdb`.`challenges` (`Title`, `ActivityType`, `ChallengeType`, `Goal`, `CreatedAt`, `UpdatedAt`) VALUES ('Mount Everest', 'Run', 'Elevation Gain', '29032', current_timestamp(), current_timestamp());
INSERT INTO `bananatestdb`.`challenges` (`Title`, `ActivityType`, `ChallengeType`, `Goal`, `CreatedAt`, `UpdatedAt`) VALUES ('Mount Everest', 'Bike', 'Elevation Gain', '29032', current_timestamp(), current_timestamp());
INSERT INTO `bananatestdb`.`challenges` (`Title`, `ActivityType`, `ChallengeType`, `Goal`, `CreatedAt`, `UpdatedAt`) VALUES ('Pacific Crest Trail', 'Run', 'Distance', '4263850', current_timestamp(), current_timestamp());
INSERT INTO `bananatestdb`.`challenges` (`Title`, `ActivityType`, `ChallengeType`, `Goal`, `CreatedAt`, `UpdatedAt`) VALUES ('NY to LA', 'Bike', 'Distance', '4489110', current_timestamp(), current_timestamp());
INSERT INTO `bananatestdb`.`challenges` (`Title`, `ActivityType`, `ChallengeType`, `Goal`, `CreatedAt`, `UpdatedAt`) VALUES ('English Channel', 'Swim', 'Distance', '33789', current_timestamp(), current_timestamp());
INSERT INTO `bananatestdb`.`challenges` (`Title`, `ActivityType`, `ChallengeType`, `Goal`, `CreatedAt`, `UpdatedAt`) VALUES ('Cuba to Florida', 'Swim', 'Distance', '176990', current_timestamp(), current_timestamp());




UPDATE `bananatestdb`.`challenges` SET `ImageFileName` = 'everest.jpg' WHERE (`ChallengeId` = '1');
UPDATE `bananatestdb`.`challenges` SET `ImageFileName` = 'pct.jpg' WHERE (`ChallengeId` = '2');
UPDATE `bananatestdb`.`challenges` SET `ImageFileName` = 'losAngeles.jpg' WHERE (`ChallengeId` = '3');
UPDATE `bananatestdb`.`challenges` SET `ImageFileName` = 'everest2.jpg' WHERE (`ChallengeId` = '4');
UPDATE `bananatestdb`.`challenges` SET `ImageFileName` = 'englishChannel.jpg' WHERE (`ChallengeId` = '5');
UPDATE `bananatestdb`.`challenges` SET `ImageFileName` = 'floridaBeach.jpg' WHERE (`ChallengeId` = '6');