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
    status Status NOT NULL DEFAULT 'Pending',
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
('51cc5b50-0bc5-4bf0-80e6-d7a0492e8f53', 'Carlos', 'Pérez', 'carlos.perez@gmail.com'),
('c671df92-0964-4843-b525-42a43c85a600', 'Maria', 'Lopez', 'maria.lopez@gmail.com'),
('b3f7d489-5b9f-42ea-bc6b-691d98c983a2', 'Jorge', 'Gonzalez', 'jorge.gonzalez@gmail.com'),
('d4e8f594-6c3f-46ea-abc6-792f09d9c8b3', 'Ana', 'Mendoza', 'ana.mendoza@gmail.com'),
('e5f96224-7d8e-48ea-bf7b-893fa0e0b9b4', 'Luis', 'Martinez', 'luis.martinez@gmail.com');

INSERT INTO route (id, transport_id) VALUES
('907a1ffa-1075-46b1-ab0c-b97809967002', '153c86c8-b237-46b5-8bd4-9aa27fe4ff04'),
('84492af2-1cb2-417a-a750-e931e764470e', 'a983a1dc-ffd1-42f9-88b8-c28bf6dbbd30'),
('c327a84e-fbb6-4ffd-badd-a4793c2a0151', 'c7db2050-17ac-4063-94f4-d33419a15caf'),
('4b9a14b7-b827-4349-a136-f369760576f5', 'b170783b-59fa-4345-9aec-0a37044905c8'),
('d8a4c1ec-f80b-4e30-8c8e-8ce5cfe8c41a', '6bdc051d-574b-478e-a7c5-ed8deabc9cf4');

INSERT INTO delivery_point (id, latitude, longitude) VALUES
('e4eadcca-bd02-4f94-aae0-dd469300aa75', -16.500000, -68.150000),
('ca1d0420-defd-40fc-8e35-d1190ecc50f5', -17.393511, -66.145981),
('3cefbb06-e2e2-48f0-a765-3eb98e696dca', -19.033320, -65.262740),
('1404d8ab-092a-467d-9faa-491e7156e806', -17.783327, -63.182129),
('6b13b84c-6d92-4736-8fb6-d63ec45408dc', -18.478333, -66.486944);

INSERT INTO client_order (id, route_id, client_id, delivery_point_id) VALUES
('cfe6ef50-da1b-4173-8f7a-45e9307956dc', '907a1ffa-1075-46b1-ab0c-b97809967002','51cc5b50-0bc5-4bf0-80e6-d7a0492e8f53', 'e4eadcca-bd02-4f94-aae0-dd469300aa75'),
('135df060-afd2-4c50-8fd8-30742f048b62', '907a1ffa-1075-46b1-ab0c-b97809967002', 'c671df92-0964-4843-b525-42a43c85a600', 'ca1d0420-defd-40fc-8e35-d1190ecc50f5'),
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

