CREATE TABLE IF NOT EXISTS client (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    email TEXT NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL
);

CREATE TABLE IF NOT EXISTS route (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    status INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL,
    transport_id UUID
);

CREATE TABLE IF NOT EXISTS delivery_point (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    latitude DOUBLE PRECISION NOT NULL,
    longitude DOUBLE PRECISION NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL
);

CREATE TABLE IF NOT EXISTS client_order (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    status INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL,
    route_id UUID REFERENCES route(id),
    client_id UUID REFERENCES client(id),
    delivery_point_id UUID REFERENCES delivery_point(id) UNIQUE
);

CREATE TABLE IF NOT EXISTS product (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT NOT NULL,
    description TEXT NOT NULL,
    weight_gr INTEGER NOT NULL CHECK (weight_gr >= 0),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL
);

CREATE TABLE IF NOT EXISTS client_order_product (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    quantity INTEGER NOT NULL CHECK (quantity > 0),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL,
    product_id UUID REFERENCES product(id),
    client_order_id UUID REFERENCES client_order(id)
);

INSERT INTO client (id, name, last_name, email) VALUES
('c1d3e678-9b6f-47e1-b0b6-a5f11e7e67a1', 'Manuel', 'Morales', 'morales.patty.jose@gmail.com'),
('a2b6e412-4b7e-45c9-a78e-58f0e4e54b2d', 'Miguel', 'Romero', 'mikyromesa100503@gmail.com'),
('b3f7d489-5b9f-42ea-bc6b-691d98c983a2', 'Karina', 'Aguirre', 'manuel_patty@hotmail.com'),
('d4e8f594-6c3f-46ea-abc6-792f09d9c8b3', 'Jorge', 'Heredia', 'morales.patty.manuel@gmail.com'),
('e5f96224-7d8e-48ea-bf7b-893fa0e0b9b4', 'Jefersson', 'Coronel', 'mikyromesa100503@gmail.com');

INSERT INTO route (id, transport_id) VALUES
('907a1ffa-1075-46b1-ab0c-b97809967002', 'eca2e142-95e6-4f36-ae9b-6b0ff4eb63bc'),
('84492af2-1cb2-417a-a750-e931e764470e', 'eab5c096-8bb4-4966-ae9c-2d58ecb4ae89'),
('c327a84e-fbb6-4ffd-badd-a4793c2a0151', 'fa368cfc-05f0-4021-bcef-3f27c6df7092'),
('4b9a14b7-b827-4349-a136-f369760576f5', '7f1c311d-e732-4dd1-9d4b-38490d2ff432'),
('d8a4c1ec-f80b-4e30-8c8e-8ce5cfe8c41a', '5e278c18-8041-4014-9fa7-5f8c72e20d8b');

INSERT INTO delivery_point (id, latitude, longitude) VALUES
('e4eadcca-bd02-4f94-aae0-dd469300aa75', -16.500000, -68.150000),
('ca1d0420-defd-40fc-8e35-d1190ecc50f5', -17.393511, -66.145981),
('3cefbb06-e2e2-48f0-a765-3eb98e696dca', -19.033320, -65.262740),
('1404d8ab-092a-467d-9faa-491e7156e806', -17.783327, -63.182129),
('6b13b84c-6d92-4736-8fb6-d63ec45408dc', -18.478333, -66.486944);

INSERT INTO client_order (id, route_id, client_id, delivery_point_id) VALUES
('cfe6ef50-da1b-4173-8f7a-45e9307956dc', '907a1ffa-1075-46b1-ab0c-b97809967002','c1d3e678-9b6f-47e1-b0b6-a5f11e7e67a1', 'e4eadcca-bd02-4f94-aae0-dd469300aa75'),
('135df060-afd2-4c50-8fd8-30742f048b62', '907a1ffa-1075-46b1-ab0c-b97809967002', 'a2b6e412-4b7e-45c9-a78e-58f0e4e54b2d', 'ca1d0420-defd-40fc-8e35-d1190ecc50f5'),
('aad9f084-8043-498a-87ef-ce2c2fe76ded', '907a1ffa-1075-46b1-ab0c-b97809967002', 'b3f7d489-5b9f-42ea-bc6b-691d98c983a2', '3cefbb06-e2e2-48f0-a765-3eb98e696dca'),
('561d618f-46c2-4bee-b55e-ee23ddc3290b', 'd8a4c1ec-f80b-4e30-8c8e-8ce5cfe8c41a','d4e8f594-6c3f-46ea-abc6-792f09d9c8b3', '1404d8ab-092a-467d-9faa-491e7156e806'),
('439bac98-f558-491d-8ee4-5ce9f2486195', 'd8a4c1ec-f80b-4e30-8c8e-8ce5cfe8c41a','e5f96224-7d8e-48ea-bf7b-893fa0e0b9b4', '6b13b84c-6d92-4736-8fb6-d63ec45408dc');

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
