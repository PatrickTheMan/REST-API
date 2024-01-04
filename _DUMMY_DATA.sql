USE Buy2Sell
GO
INSERT INTO Brand
VALUES 
('Siemens'),
('Allen Bradley'),
('Bosch');
INSERT INTO BrandAlias
VALUES
('SI', 1),
('SM', 1),
('AB', 2),
('ALB', 2),
('BSH', 3),
('BOS', 3);
INSERT INTO ItemGroup
VALUES
('Pneumatics'),
('Actuators');
INSERT INTO Product
VALUES
('Pressure regulator', '3RK1901-1AA00', '3RK1901', '4011209534490', '783087661097', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue nisl ac euismod rhoncus. Praesent sit amet elit fermentum, commodo nunc ut, malesuada dolor. Duis auctor nisl in libero feugiat mollis. Aenean condimentum metus at ornare viverra. Mauris congue laoreet massa, et volutpat orci egestas vitae. Integer eu aliquet ante, quis laoreet leo. Morbi sagittis massa eu tortor vehicula rutrum. Vestibulum quam turpis, rhoncus in urna ac, convallis sagittis erat. Nunc dapibus finibus nulla, viverra congue erat varius nec. Duis dapibus, odio consectetur porttitor dapibus, dui libero gravida velit, non viverra diam ipsum sollicitudin massa.', 1, 1, NULL),
('Industrial Shock Absorber', '3RK1901-8J34T', '3RK1901', '6901085850857', '656314161176', 'Curabitur velit ex, finibus non diam nec, semper mollis quam. Curabitur vitae velit quis turpis venenatis consectetur porta nec lorem. Pellentesque rhoncus neque dolor, id dictum ex feugiat a. Proin nisi elit, luctus non porttitor id, venenatis non tellus. Nullam sollicitudin facilisis felis, at pulvinar magna laoreet a. Nunc vestibulum iaculis mi faucibus mollis. Pellentesque pellentesque urna eu lorem dignissim consectetur. Morbi tincidunt risus nec gravida mollis. Suspendisse purus ex, congue suscipit enim pharetra, blandit fermentum dui. Nunc imperdiet a odio et aliquam.', 1, 1, NULL),
('Flow controller', '8494-40O51', '8494', '9828123599332', '692971051881', 'Proin mollis pretium purus, sed sodales arcu consectetur ac. Vivamus a ipsum vitae massa scelerisque mollis. Integer sed velit feugiat, mattis tortor quis, volutpat orci. In hac habitasse platea dictumst. Fusce aliquet id magna dapibus efficitur. Aliquam enim sapien, facilisis a erat non, pharetra sodales purus. Vivamus iaculis eget mauris id porta. Mauris ullamcorper nibh at orci aliquet ultricies.', 1, 2, NULL),
('Servo motor', '8494-4OALR', '8494', '1915107309218', '098831608909', 'Duis fermentum faucibus elit, vitae imperdiet lorem egestas sed. Maecenas mollis sed ante quis commodo. In nec dolor ut tellus suscipit pellentesque fermentum porta mauris. Duis tristique elit ac consequat tincidunt. Donec non fringilla ex. Phasellus massa ipsum, posuere sit amet convallis vitae, sodales ac dolor. Quisque sed nisi ac magna maximus commodo quis vitae libero. Nam a justo id nibh condimentum consequat vulputate vitae lacus.', 2, 2, NULL),
('220mm DC motor', '4412105201-F7V6H', '4412105201', '1354591800084', '859068719505', 'Praesent interdum ex ut libero pretium placerat. In pretium dolor nunc, sed egestas purus laoreet ac. Duis lorem augue, dignissim et ex ac, scelerisque ornare augue. Donec vitae scelerisque sapien. Maecenas dapibus fringilla interdum. Phasellus convallis purus ut nisl euismod lacinia. Interdum et malesuada fames ac ante ipsum primis in faucibus. Suspendisse porta sed mauris ut rhoncus. Sed eleifend tincidunt semper. Vestibulum ultricies tellus in tellus rutrum ornare. Donec enim risus, facilisis ac hendrerit vitae, venenatis pulvinar mi.', 2, 3, NULL),
('Stepping motor', '4412105202-KQWWR', '4412105202', '5735273680191', '952197146213', 'Donec eu mattis felis, dapibus elementum tortor. In et tellus auctor, dictum dui eu, scelerisque lectus. Praesent tempus odio neque, auctor faucibus odio tempor in. Aliquam ultricies tempor nisi vitae facilisis. Quisque eu elit rhoncus dolor rhoncus suscipit eu laoreet diam. Nunc eleifend, est ac vestibulum aliquam, mauris elit tempor augue, non blandit sem ex a felis. Fusce a volutpat magna.', 2, 3, NULL);