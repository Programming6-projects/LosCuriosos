CREATE TYPE order_status AS ENUM ('Pending', 'Shipped', 'Delivered', 'Cancelled');

CREATE TABLE IF NOT EXISTS client (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    email TEXT NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL
);

CREATE TABLE IF NOT EXISTS transport
(
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT NOT NULL,
    capacity_gr INTEGER NOT NULL,
    available_units INTEGER NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL
);

CREATE TABLE IF NOT EXISTS client_order_status (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL
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

CREATE TABLE IF NOT EXISTS client_order (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL,
    client_id UUID REFERENCES client(id),
    client_order_status_id UUID REFERENCES client_order_status(id)
);

CREATE TABLE IF NOT EXISTS delivery_point (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    latitude DOUBLE PRECISION NOT NULL,
    longitude DOUBLE PRECISION NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL,
    client_order_id UUID REFERENCES client_order(id)
);

CREATE TABLE IF NOT EXISTS client_order_products(
    client_order_id UUID REFERENCES client_order(id),
    product_id UUID REFERENCES product(id),
    quantity INTEGER NOT NULL,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL,
    PRIMARY KEY (client_order_id, product_id)
);

CREATE TABLE IF NOT EXISTS client_order_transport (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL,
    client_order_id UUID REFERENCES client_order(id),
    transport_id UUID REFERENCES transport(id)
);

CREATE TABLE IF NOT EXISTS route (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ DEFAULT NULL,
    status order_status NOT NULL,
    transport_id UUID REFERENCES transport(id)
);


INSERT INTO client (id, name, last_name, email) VALUES
('c1d3e678-9b6f-47e1-b0b6-a5f11e7e67a1', 'Carlos', 'Pérez', 'carlos.perez@gmail.com'),
('a2b6e412-4b7e-45c9-a78e-58f0e4e54b2d', 'Maria', 'Lopez', 'maria.lopez@gmail.com'),
('b3f7d489-5b9f-42ea-bc6b-691d98c983a2', 'Jorge', 'Gonzalez', 'jorge.gonzalez@gmail.com'),
('d4e8f594-6c3f-46ea-abc6-792f09d9c8b3', 'Ana', 'Mendoza', 'ana.mendoza@gmail.com'),
('e5f96224-7d8e-48ea-bf7b-893fa0e0b9b4', 'Luis', 'Martinez', 'luis.martinez@gmail.com');

INSERT INTO transport (id, name, capacity_gr, available_units) VALUES
('f1a2b3c4-d234-5678-9101-112131415161', 'Truck - Large', 10000000, 3),
('f2a3b4c5-e234-5678-9201-112131415162', 'Truck - Medium', 7000000, 4),
('f3a4b5c6-f234-5678-9301-112131415163', 'Van', 3500000, 5);

INSERT INTO client_order_status (id, name) VALUES
('8487d2c0-93db-418f-a9c8-ac3fc422428b', 'Pending'),
('7683ba92-0f00-4000-b86c-f51273ce34b8', 'Shipped'),
('315427f7-e8ff-4b2e-ba25-d50c08260b18', 'Delivered'),
('ba80ef87-df15-4266-9450-d446a1f43de4', 'Cancelled');

INSERT INTO product (id, name, description, weight_gr) VALUES
('3842f419-4c13-4880-827b-e784d9ad0b07', 'Pepsi', 'A popular carbonated soft drink', 500),
('da27b4cb-94c1-42d0-a914-e600559ade80', 'Pepsi Black', 'A zero-calorie, sugar-free variant of Pepsi', 500),
('5d609b2d-99b0-4c1d-bd25-7cc7984feba7', 'Guarana', 'A Brazilian soft drink made from the guarana fruit', 600),
('f55b772e-fbf8-494d-9b9a-8bd7a028f9e4', 'Pacena', 'A traditional Bolivian beer', 1000),
('07580c62-faaa-42f3-b69d-1e03c3a0cc62', 'Chicha', 'A traditional Andean fermented drink', 2000),
('fe1198c3-4496-45b3-bdf3-93b7e13a54b5', 'Huari', 'A popular Bolivian beer', 1000);

INSERT INTO client_order (id, client_id, client_order_status_id) VALUES
('cfe6ef50-da1b-4173-8f7a-45e9307956dc', 'c1d3e678-9b6f-47e1-b0b6-a5f11e7e67a1', '8487d2c0-93db-418f-a9c8-ac3fc422428b'),
('135df060-afd2-4c50-8fd8-30742f048b62', 'a2b6e412-4b7e-45c9-a78e-58f0e4e54b2d', '7683ba92-0f00-4000-b86c-f51273ce34b8'),
('aad9f084-8043-498a-87ef-ce2c2fe76ded', 'b3f7d489-5b9f-42ea-bc6b-691d98c983a2', '8487d2c0-93db-418f-a9c8-ac3fc422428b'),
('561d618f-46c2-4bee-b55e-ee23ddc3290b', 'd4e8f594-6c3f-46ea-abc6-792f09d9c8b3', '315427f7-e8ff-4b2e-ba25-d50c08260b18'),
('439bac98-f558-491d-8ee4-5ce9f2486195', 'e5f96224-7d8e-48ea-bf7b-893fa0e0b9b4', '7683ba92-0f00-4000-b86c-f51273ce34b8');

INSERT INTO delivery_point (id, latitude, longitude, client_order_id) VALUES
('e4eadcca-bd02-4f94-aae0-dd469300aa75', -16.500000, -68.150000, 'cfe6ef50-da1b-4173-8f7a-45e9307956dc'),
('ca1d0420-defd-40fc-8e35-d1190ecc50f5', -17.393511, -66.145981, '135df060-afd2-4c50-8fd8-30742f048b62'),
('3cefbb06-e2e2-48f0-a765-3eb98e696dca', -19.033320, -65.262740, 'aad9f084-8043-498a-87ef-ce2c2fe76ded'),
('1404d8ab-092a-467d-9faa-491e7156e806', -17.783327, -63.182129, '561d618f-46c2-4bee-b55e-ee23ddc3290b'),
('6b13b84c-6d92-4736-8fb6-d63ec45408dc', -18.478333, -66.486944, '439bac98-f558-491d-8ee4-5ce9f2486195');

INSERT INTO client_order_products (client_order_id, product_id, quantity) VALUES
('cfe6ef50-da1b-4173-8f7a-45e9307956dc', '3842f419-4c13-4880-827b-e784d9ad0b07', 200),
('cfe6ef50-da1b-4173-8f7a-45e9307956dc', 'da27b4cb-94c1-42d0-a914-e600559ade80', 150),
('135df060-afd2-4c50-8fd8-30742f048b62', '5d609b2d-99b0-4c1d-bd25-7cc7984feba7', 400),
('aad9f084-8043-498a-87ef-ce2c2fe76ded', 'f55b772e-fbf8-494d-9b9a-8bd7a028f9e4', 300),
('561d618f-46c2-4bee-b55e-ee23ddc3290b', '07580c62-faaa-42f3-b69d-1e03c3a0cc62', 100),
('439bac98-f558-491d-8ee4-5ce9f2486195', 'fe1198c3-4496-45b3-bdf3-93b7e13a54b5', 500);

INSERT INTO client_order_transport (id, client_order_id, transport_id) VALUES
('6cc9ea05-33b2-4310-b1ee-dac9e2b3fbe0', 'cfe6ef50-da1b-4173-8f7a-45e9307956dc', 'f1a2b3c4-d234-5678-9101-112131415161'),
('d2b75a69-ad7a-4472-b005-a1153797f0be', '135df060-afd2-4c50-8fd8-30742f048b62', 'f2a3b4c5-e234-5678-9201-112131415162'),
('567fbf13-ba76-40d2-9fff-1700f9775ada', 'aad9f084-8043-498a-87ef-ce2c2fe76ded', 'f3a4b5c6-f234-5678-9301-112131415163'),
('181ae177-d702-4c4b-af59-181c48e74e70', '561d618f-46c2-4bee-b55e-ee23ddc3290b', 'f1a2b3c4-d234-5678-9101-112131415161'),
('7e68ebc8-f285-4f16-acd7-09905958859a', '439bac98-f558-491d-8ee4-5ce9f2486195', 'f2a3b4c5-e234-5678-9201-112131415162');

INSERT INTO route (id, status) VALUES
('940bf5ba-f0dd-4ed8-9491-e95a35d0bd49', 'Pending'),
('7b0743e0-7117-4f4b-9a0b-e2d76a9369ac', 'Shipped'),
('a048a326-bc1a-42ad-9bd3-a0f47752b52b', 'Delivered'),
('792e4ed8-1122-4988-ab54-19b60449a466', 'Cancelled'),
('fc3538ec-f378-4383-bd77-95dc2b5dfd3c', 'Pending');
