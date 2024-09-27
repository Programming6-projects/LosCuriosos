INSERT INTO client (id, name, last_name, email) VALUES
('c1d3e678-9b6f-47e1-b0b6-a5f11e7e67a1', 'Manuel', 'Morales', 'morales.patty.jose@gmail.com'),
('a2b6e412-4b7e-45c9-a78e-58f0e4e54b2d', 'Miguel', 'Romero', 'mikyromesa100503@gmail.com'),
('b3f7d489-5b9f-42ea-bc6b-691d98c983a2', 'Karina', 'Aguirre', 'manuel_patty@hotmail.com'),
('d4e8f594-6c3f-46ea-abc6-792f09d9c8b3', 'Jorge', 'Heredia', 'morales.patty.manuel@gmail.com'),
('e5f96224-7d8e-48ea-bf7b-893fa0e0b9b4', 'Jefersson', 'Coronel', 'jefersoncoronel700@gmail.com');

INSERT INTO route (id, transport_id) VALUES
('907a1ffa-1075-46b1-ab0c-b97809967002', 'eca2e142-95e6-4f36-ae9b-6b0ff4eb63bc'),
('84492af2-1cb2-417a-a750-e931e764470e', 'eab5c096-8bb4-4966-ae9c-2d58ecb4ae89'),
('c327a84e-fbb6-4ffd-badd-a4793c2a0151', 'fa368cfc-05f0-4021-bcef-3f27c6df7092'),
('4b9a14b7-b827-4349-a136-f369760576f5', '7f1c311d-e732-4dd1-9d4b-38490d2ff432'),
('d8a4c1ec-f80b-4e30-8c8e-8ce5cfe8c41a', '5e278c18-8041-4014-9fa7-5f8c72e20d8b');

INSERT INTO strike (id, description, transport_id) VALUES
('176b62f0-982c-4140-917b-aede8fd47da0', 'The Transport was behind schedule 24/09/24', 'eca2e142-95e6-4f36-ae9b-6b0ff4eb63bc'),
('f891b54e-e5be-4fb4-967c-8eb81f0c8f33', 'The Transport was behind schedule 22/09/24', 'eca2e142-95e6-4f36-ae9b-6b0ff4eb63bc'),
('9fb294d0-4d44-4384-837d-967715b1077e', 'The Transport was behind schedule 02/09/24', 'eca2e142-95e6-4f36-ae9b-6b0ff4eb63bc'),
('55d3d6ee-2e7d-41cf-8e34-b0e4ee3166b5', 'The Transport was behind schedule 15/09/24', 'eab5c096-8bb4-4966-ae9c-2d58ecb4ae89'),
('cc8e071d-bb65-4d98-98d6-76dca01c0c3d', 'The Transport was behind schedule 17/09/24', 'fa368cfc-05f0-4021-bcef-3f27c6df7092'),
('028b54c8-ec30-4e4e-a7ef-6aeb12d9060e', 'The Transport was behind schedule 18/09/24', '7f1c311d-e732-4dd1-9d4b-38490d2ff432'),
('f703575b-bb2c-4f7f-b2ff-ab06f4c5d369', 'The Transport was behind schedule 12/09/24', '5e278c18-8041-4014-9fa7-5f8c72e20d8b');

INSERT INTO delivery_point (id, latitude, longitude) VALUES
('e4eadcca-bd02-4f94-aae0-dd469300aa75', -16.499927, -68.149987),
('ca1d0420-defd-40fc-8e35-d1190ecc50f5', -17.392442, -66.146213),
('3cefbb06-e2e2-48f0-a765-3eb98e696dca', -19.033349, -65.262781),
('1404d8ab-092a-467d-9faa-491e7156e806', -17.783368, -63.182643),
('6b13b84c-6d92-4736-8fb6-d63ec45408dc', -18.481899, -66.484528);

INSERT INTO client_order (id, route_id, client_id, delivery_point_id, delivery_time) VALUES
('cfe6ef50-da1b-4173-8f7a-45e9307956dc', '907a1ffa-1075-46b1-ab0c-b97809967002','c1d3e678-9b6f-47e1-b0b6-a5f11e7e67a1', 'e4eadcca-bd02-4f94-aae0-dd469300aa75', NULL),
('135df060-afd2-4c50-8fd8-30742f048b62', '907a1ffa-1075-46b1-ab0c-b97809967002', 'a2b6e412-4b7e-45c9-a78e-58f0e4e54b2d', 'ca1d0420-defd-40fc-8e35-d1190ecc50f5', NULL),
('aad9f084-8043-498a-87ef-ce2c2fe76ded', '907a1ffa-1075-46b1-ab0c-b97809967002', 'b3f7d489-5b9f-42ea-bc6b-691d98c983a2', '3cefbb06-e2e2-48f0-a765-3eb98e696dca', NULL),
('561d618f-46c2-4bee-b55e-ee23ddc3290b', 'd8a4c1ec-f80b-4e30-8c8e-8ce5cfe8c41a','d4e8f594-6c3f-46ea-abc6-792f09d9c8b3', '1404d8ab-092a-467d-9faa-491e7156e806', NULL),
('439bac98-f558-491d-8ee4-5ce9f2486195', 'd8a4c1ec-f80b-4e30-8c8e-8ce5cfe8c41a','e5f96224-7d8e-48ea-bf7b-893fa0e0b9b4', '6b13b84c-6d92-4736-8fb6-d63ec45408dc', NULL);

INSERT INTO product (id, name, description, weight_gr) VALUES
('3842f419-4c13-4880-827b-e784d9ad0b07', 'Pepsi', 'A popular carbonated soft drink', 500),
('da27b4cb-94c1-42d0-a914-e600559ade80', 'Pepsi Black', 'A zero-calorie, sugar-free variant of Pepsi', 500),
('5d609b2d-99b0-4c1d-bd25-7cc7984feba7', 'Guarana', 'A Brazilian soft drink made from the guarana fruit', 600),
('f55b772e-fbf8-494d-9b9a-8bd7a028f9e4', 'Pacena', 'A traditional Bolivian beer', 1000),
('07580c62-faaa-42f3-b69d-1e03c3a0cc62', 'Chicha', 'A traditional Andean fermented drink', 2000),
('fe1198c3-4496-45b3-bdf3-93b7e13a54b5', 'Huari', 'A popular Bolivian beer', 1000);

INSERT INTO client_order_product (id, client_order_id, product_id, quantity) VALUES
('5dd996e4-e9a2-45b4-a8ba-22f3bfbd95a6', 'cfe6ef50-da1b-4173-8f7a-45e9307956dc', '3842f419-4c13-4880-827b-e784d9ad0b07', 200),
('15c15425-aa03-4107-967d-1164e0143064', 'cfe6ef50-da1b-4173-8f7a-45e9307956dc', 'da27b4cb-94c1-42d0-a914-e600559ade80', 150),
('409222cd-2e2c-4c56-8e87-9d334bb337c7', '135df060-afd2-4c50-8fd8-30742f048b62', '5d609b2d-99b0-4c1d-bd25-7cc7984feba7', 400),
('46f2e93a-6b10-4817-82fc-a41d172023f4', 'aad9f084-8043-498a-87ef-ce2c2fe76ded', 'f55b772e-fbf8-494d-9b9a-8bd7a028f9e4', 300),
('01811f65-50dc-4d60-b167-9399d9b87d13', '561d618f-46c2-4bee-b55e-ee23ddc3290b', '07580c62-faaa-42f3-b69d-1e03c3a0cc62', 100),
('2f9b3526-d1df-4aa3-964a-05c6f364e41f', '439bac98-f558-491d-8ee4-5ce9f2486195', 'fe1198c3-4496-45b3-bdf3-93b7e13a54b5', 500);
