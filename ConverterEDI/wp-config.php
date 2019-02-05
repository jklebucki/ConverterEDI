<?php
/**
 * The base configuration for WordPress
 *
 * The wp-config.php creation script uses this file during the
 * installation. You don't have to use the web site, you can
 * copy this file to "wp-config.php" and fill in the values.
 *
 * This file contains the following configurations:
 *
 * * MySQL settings
 * * Secret keys
 * * Database table prefix
 * * ABSPATH
 *
 * @link https://codex.wordpress.org/Editing_wp-config.php
 *
 * @package WordPress
 */

define('WP_HOME','https://dyskontpaliwowy.pl');
define('WP_SITEURL','https://dyskontpaliwowy.pl');

// ** MySQL settings - You can get this info from your web host ** //
/** The name of the database for WordPress */
define('DB_NAME', '05826964_0000012');

/** MySQL database username */
define('DB_USER', '05826964_0000012');

/** MySQL database password */
define('DB_PASSWORD', 'G!3q7HzEX.6h');

/** MySQL hostname */
define('DB_HOST', 'localhost');

/** Database Charset to use in creating database tables. */
define('DB_CHARSET', 'utf8mb4');

/** The Database Collate type. Don't change this if in doubt. */
define('DB_COLLATE', '');

/**#@+
 * Authentication Unique Keys and Salts.
 *
 * Change these to different unique phrases!
 * You can generate these using the {@link https://api.wordpress.org/secret-key/1.1/salt/ WordPress.org secret-key service}
 * You can change these at any point in time to invalidate all existing cookies. This will force all users to have to log in again.
 *
 * @since 2.6.0
 */
define('AUTH_KEY',         '@c+a$LO$E$V$ 3TG_,A#@i^ lvx/peYqzJO[r%kZK*|V5HrW]XJn>c2>t(gVA0Ow');
define('SECURE_AUTH_KEY',  'hLg=c5x4%cWr}gMxNj{e,lR!5[vQ:bwF<ih`W]dkwr2j#s9>s}em%a0r`nd;z5,]');
define('LOGGED_IN_KEY',    'C@%MVXR%*3;hFUUY|8XNa76j,d-[#R%Q!;o:rD(+h1b2_&f6g:m(JD$MRY #pE6 ');
define('NONCE_KEY',        '=1E&2u;dfFluj~:Vc_q#2Hay|?=yGTu_z>F8Iy=bbuc%/Zk-.9-uDv=P7uhR{]Tm');
define('AUTH_SALT',        '*V  9><iL4uU*}3RA,T/o3bt@4BjPDLF;}G3`_=-|%a&6~oeX,?_#sR8%IQnu<WU');
define('SECURE_AUTH_SALT', 'kg*!gGUm>FPzL_g1MNJTW~,m}FSP%]/( @H]]X{kA<f-R1+@o3D12ZEqHnQ=jsk=');
define('LOGGED_IN_SALT',   '3z=lQO2H7-pN0dch]}b#{X*t@v:T6&T?qp`E~8vlr#gOaE/y7ahT?c>c~Wy!:i6w');
define('NONCE_SALT',       '9V|VE=wEoE(h7PC_<WsmwbSZk1O#vtEYD*M *BN*OZRPMyL0=qc}<Qr4t!U(L&0R');
define('WP_HOME','http://www.dyskontpaliwowy.pl');
define('WP_SITEURL','http://www.dyskontpaliwowy.pl');

/**#@-*/

/**
 * WordPress Database Table prefix.
 *
 * You can have multiple installations in one database if you give each
 * a unique prefix. Only numbers, letters, and underscores please!
 */
$table_prefix  = 'dys_';

/**
 * For developers: WordPress debugging mode.
 *
 * Change this to true to enable the display of notices during development.
 * It is strongly recommended that plugin and theme developers use WP_DEBUG
 * in their development environments.
 *
 * For information on other constants that can be used for debugging,
 * visit the Codex.
 *
 * @link https://codex.wordpress.org/Debugging_in_WordPress
 */
define('WP_DEBUG', false);

/* That's all, stop editing! Happy blogging. */

/** Absolute path to the WordPress directory. */
if ( !defined('ABSPATH') )
	define('ABSPATH', dirname(__FILE__) . '/');

/** Sets up WordPress vars and included files. */
require_once(ABSPATH . 'wp-settings.php');
