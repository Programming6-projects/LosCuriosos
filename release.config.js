const config = {
    branches: ['main'],
    plugins: [
        [
            '@semantic-release/commit-analyzer',
            {
                releaseRules: [
                    { type: 'feat', release: 'minor' },
                    { type: 'fix', release: 'patch' },
                    { type: 'ci', release: 'minor' },
                    { type: 'chore', release: 'minor' },
                    { type: 'docs', release: 'minor' },
                    { type: 'test', release: 'patch' },
                    { type: 'perf', release: 'minor' }
                ]
            }
        ],
        '@semantic-release/release-notes-generator',
        [
            '@semantic-release/changelog',
            {
                changelogFile: 'CHANGELOG.md',
                changelogTitle: '# Changelog\n\n'
            }
        ],
        [
            '@semantic-release/npm',
            {
                npmPublish: false
            }
        ],
        [
            '@semantic-release/git',
            {
                assets: ['package.json', 'CHANGELOG.md'],
                message: 'chore(release): ${nextRelease.version} [skip ci]\n\n${nextRelease.notes}'
            }
        ],
        '@semantic-release/github'
    ]
};

module.exports = config;
